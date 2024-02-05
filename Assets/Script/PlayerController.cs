using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int[] dy = { -1, 0, 1, 0 };
    private int[] dx = { 0, 1, 0, -1 };

    [Header("info")]
    [SerializeField] private SpriteRenderer select_Sprite;   // 선택한 오브젝트를 출력할 SpriteRenderer
    [SerializeField] private GameObject select_Object;       // 선택한 오브젝트
    [SerializeField] private int install_Cost;
    [SerializeField] private int install_type;
    [SerializeField] private int mouse_type;

    private StageData stage_data;

    [Header("Mouse Info")]
    [SerializeField] private int cur_y;
    [SerializeField] private int cur_x;
    private void Start()
    {
        // 초기화
        select_Object = null;
        select_Sprite.sprite = null;
        install_Cost = 0;
        install_type = -1;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            CancelTrap();               // 오브젝트 선택 취소
        }

        if (select_Object != null)
            MouseController();
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
        else // 돈 부족
        {
            // 돈 부족 text 2초간 출력
        }
    }

    void CancelTrap()
    {
        select_Object = null;
        select_Sprite.sprite = null;
        install_Cost = 0;
        install_type = -1;
    }

    void MouseController()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        transform.position = mousePosition;

        checkedMouse();

        InstallTrap();
    }

    void InstallTrap()
    {
        if (Input.GetMouseButtonDown(0) && select_Sprite.color == Color.green)
        {
            Instantiate(select_Object, transform.position, Quaternion.identity);
            GameManager.Instance.UpdateGold(-install_Cost);
            CancelTrap();
        }
    }

    void checkedMouse()
    {
        if (stage_data == null)
            return;

        cur_y = (int)transform.position.y;
        cur_x = (int)transform.position.x;

        // 범위 밖
        if (cur_x < 0 || cur_y < 0 || cur_x >= stage_data.size_x || cur_y >= stage_data.size_y)
        {
            select_Sprite.color = Color.red;
            return;
        }


        if (stage_data.pos[cur_y, cur_x] == install_type && checkAdjacent(cur_y, cur_x)) // 설치 가능한 위치
        {
            select_Sprite.color = Color.green;
        }
        else // 설치 불가능한 위치
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
    }

    /*
 * ## TODO
 * 설치 타입에 따른 설치 가능 ( 초록) / 불가능(빨강) 표시
 * 
 */
}
