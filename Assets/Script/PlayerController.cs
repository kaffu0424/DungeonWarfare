using System.Net;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public enum TrapDirection
{
    BOTTOM,
    RIGHT,
    TOP,
    LEFT,
    NONE
}

public class PlayerController : MonoBehaviour
{
    private string directionArrowTag = "DirectionArrow";
    private int[] dy = { -1, 0, 1, 0 };
    private int[] dx = { 0, 1, 0, -1 };

    [Header("Unity")]
    [SerializeField] private Transform trap_parent;

    [Header("Select Trap infomation")]
    [SerializeField] private SpriteRenderer select_Sprite;  // ������ ������ SpriteRenderer
    [SerializeField] private GameObject select_Object;      // ������ ����
    [SerializeField] private int install_Cost;              // ������ ���� ����
    [SerializeField] private InstallType install_type;      // ������ ���� ��ġ Ÿ��
    [SerializeField] private bool doing_install;            // ���� ��ġ�� ����
    [SerializeField] private TrapDirection trap_direction;

    [Header("Mouse Info")]
    [SerializeField] private int cur_y;
    [SerializeField] private int cur_x;

    [Header("Stage Infomation")]
    private StageData stage_data;
    private bool[,] install_positionData;
    private void Start()
    {
        CancelTrap();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            CancelTrap();               // ���� ���� ���
        }

        if (select_Object != null)      // ���õ� ������ ������
            MouseController();          // ���콺 ������
    }

    public void SelectTrap(int _id)
    {
        int cost = TrapManager.Instance.GetTrapCost(_id);
        int curGold = GameManager.Instance.p_gold;
        if (curGold >= cost)
        {
            select_Sprite.sprite = TrapManager.Instance.GetTrapSprite(_id);
            select_Object = TrapManager.Instance.GetTrapObject(_id);
            install_Cost = cost;
            install_type = TrapManager.Instance.GetTrapType(_id);     
        }
        else // �� ����
        {
            // �� ���� text 2�ʰ� ���
        }
    }

    void CancelTrap()
    {
        select_Object = null;
        select_Sprite.sprite = null;
        select_Sprite.transform.parent = this.transform;
        select_Sprite.transform.localPosition = Vector3.zero;

        for(int i = 0; i < select_Sprite.transform.childCount; i++)
            select_Sprite.transform.GetChild(i).gameObject.SetActive(false);

        install_Cost = 0;
        install_type = InstallType.NONE;

        trap_direction = TrapDirection.NONE;

        doing_install = false;
    }

    void MouseController()
    {
        // ���콺 ��ĭ�� �̵�
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        transform.position = mousePosition;

        if(doing_install) // ��ġ��
        {
            InstallTrapDirection(); // ��ġ ���� ����
            return; // ��ġ ��� || ��ġ �Ϸ� ������ return
        } 

        CheckCanInstall(); // ��ġ �������� ���� Ȯ��.
        InstallTrap(); // ��ġ�ϱ� ,  �ٴ��� ��� ��ġ, ���� ��ġ������ �Ѿ 
    }

    void InstallTrapDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag(directionArrowTag))
                {
                    if (obj.name == "BOTTOM")
                        trap_direction = TrapDirection.BOTTOM;
                    else if (obj.name == "RIGHT")
                        trap_direction = TrapDirection.RIGHT;
                    else if (obj.name == "TOP")
                        trap_direction = TrapDirection.TOP;
                    if (obj.name == "LEFT")
                        trap_direction = TrapDirection.LEFT;
                }
            }

            if (trap_direction == TrapDirection.NONE)
                return;
            else if (trap_direction == TrapDirection.BOTTOM)
                install(new Vector3(0, 0, 90));
            else if (trap_direction == TrapDirection.RIGHT)
                install(new Vector3(0, 0, 180));
            else if (trap_direction == TrapDirection.TOP)
                install(new Vector3(0, 0, 270));
            else if (trap_direction == TrapDirection.LEFT)
                install(Vector3.zero);
        }
    }

    void InstallTrap()
    {
        if (Input.GetMouseButtonDown(0) && select_Sprite.color == Color.green)
        {
            doing_install = true; // ��ġ�� ���� ��ȯ
            select_Sprite.transform.parent = null;

            InstallDirection(); // ��ġ ���� Ȯ��
        }
    }

    void InstallDirection()
    {
        if (install_type == 0) // �ٴ������� ��ġ�� ��� ��ġ
        {
            install(Vector3.zero);
            return;
        }

        int y = (int)select_Sprite.transform.position.y;
        int x = (int)select_Sprite.transform.position.x;

        for (int i = 0; i < 4; i++)
        {
            int ny = y + dy[i];
            int nx = x + dx[i];

            if (ny < 0 || nx < 0 || ny >= stage_data.size_y || nx >= stage_data.size_x)
                continue;

            if (stage_data.pos[ny,nx] == 0)
                select_Sprite.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void install(Vector3 rotate)
    {
        install_positionData[(int)select_Sprite.transform.position.y, (int)select_Sprite.transform.position.x] = true;
        GameObject obj = Instantiate(select_Object, select_Sprite.transform.position, Quaternion.Euler(rotate));
        obj.transform.parent = trap_parent;
        GameManager.Instance.UseGold(-install_Cost);
        CancelTrap();
    }

    void CheckCanInstall()
    {
        if (stage_data == null)
            return;

        cur_y = (int)transform.position.y;
        cur_x = (int)transform.position.x;

        // �ٴ� ��ġ ����
        if(install_type == InstallType.GROUND)
        {
            checkType_ground();
        }

        // �� ��ġ ����
        else if( install_type == InstallType.WALL)
        {
            checkType_wall();
        }
    }

    void checkType_ground()
    {
        if (cur_x < 0 || cur_y < 0 || cur_x >= stage_data.size_x || cur_y >= stage_data.size_y)
            return;

        if (stage_data.pos[cur_y, cur_x] == (int)install_type && !install_positionData[cur_y, cur_x])
            select_Sprite.color = Color.green;
        else
            select_Sprite.color = Color.red;
        return;
    }

    void checkType_wall()
    {
        // ���� ��
        if (cur_x < 0 || cur_y < 0 || cur_x >= stage_data.size_x || cur_y >= stage_data.size_y)
        {
            select_Sprite.color = Color.red;
            return;
        }

        if (stage_data.pos[cur_y, cur_x] == (int)install_type && checkAdjacent(cur_y, cur_x) && !install_positionData[cur_y, cur_x]) // ��ġ ������ ��ġ
        {
            select_Sprite.color = Color.green;
        }
        else // ��ġ �Ұ����� ��ġ
        {
            select_Sprite.color = Color.red;
        }
    }


    bool checkAdjacent(int y,int x)
    {
        if (install_type == 0)
            return true;

        for(int i = 0; i < 4; i++)
        {
            int ny = y + dy[i];
            int nx = x + dx[i];

            if (nx < 0 || ny < 0 || nx >= stage_data.size_x || ny >= stage_data.size_y)
                continue;

            if (stage_data.pos[ny, nx] == 0)
                return true;
        }

        return false;
    }

    public void SetStageData(StageData data)
    {
        stage_data = data;
        install_positionData = new bool[data.size_y, data.size_x];
    }

    /*
     * ## TODO
     * ��ġ Ÿ�Կ� ���� ��ġ ���� ( �ʷ�) / �Ұ���(����) ǥ��
     * 
     */
}
