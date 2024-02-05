using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("info")]
    [SerializeField] private SpriteRenderer select_Sprite;   // 선택한 오브젝트를 출력할 SpriteRenderer
    [SerializeField] private GameObject select_Object;       // 선택한 오브젝트
    [SerializeField] private int install_Cost;
    [SerializeField] private int install_type;
    [SerializeField] private int mouse_type;

    private void Start()
    {
        install_Cost = 0;
        select_Sprite = GetComponent<SpriteRenderer>();
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
            /*
             * ## TODO
             * 설치 타입에 따른 설치 가능 ( 초록) / 불가능(빨강) 표시
             * 
             */            
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

        mousePosition =
            new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));

        transform.position = mousePosition;

        InstallTrap();
    }

    void InstallTrap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(select_Object, transform.position, Quaternion.identity);
            GameManager.Instance.UpdateGold(-install_Cost);
            CancelTrap();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            Debug.Log("ground");
        }
        else if(collision.CompareTag("Wall"))
        {
            Debug.Log("wall");
        }
    }
}
