using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapManager : Singleton<TrapManager>
{
    private List<TrapData> trap_Data;

    [Header("unity")]
    [SerializeField] private List<GameObject> trap_Prefabs;
    [SerializeField] private List<Sprite> trap_Sprite;
    [SerializeField] private Transform trap_transform;

    [Header("Object pool")]
    [SerializeField] private GameObject dartBullet_prefab;
    [SerializeField] private Queue<DartBullet> dartBullets;
    [SerializeField] private GameObject boltBullet_prefab;
    [SerializeField] private Queue<BoltBullet> boltBullets;

    protected override void InitManager()
    {
        // ��ƮƮ�� -> �ѹ��� ������ �������� ����

        trap_Data = new List<TrapData>
        {
            new TrapData(0, "��Ʈ Ʈ��", 2, 0.65f, 500, 9999, 3, InstallType.WALL),
            new TrapData(1, "������ũ Ʈ��", 3, 2.0f, 650, 9999, 0, InstallType.GROUND),
            new TrapData(2, "��Ʈ Ʈ��", 18, 3.5f, 700, 9999, 3, InstallType.WALL),
            new TrapData(3, "������ Ʈ��", 0, 0.0f, 300, 9999, 0, InstallType.GROUND),
            new TrapData(4, "Ǫ�� Ʈ��", 2, 5.0f, 500, 9999, 1, InstallType.WALL),
            new TrapData(5, "�ۻ� Ʈ��", 3, 4.0f, 200, 9999, 4, InstallType.WALL)
        };


        dartBullets = new Queue<DartBullet>();
        for(int i = 0; i < 5; i++)
        {
            DartBullet dartBullet = Instantiate(dartBullet_prefab, this.transform).GetComponent<DartBullet>();
            dartBullet.gameObject.SetActive(false);
            dartBullets.Enqueue(dartBullet);
        }

        boltBullets = new Queue<BoltBullet>();
        for (int i = 0; i < 5; i++)
        {
            BoltBullet boltBullet = Instantiate(boltBullet_prefab, this.transform).GetComponent<BoltBullet>();
            boltBullet.gameObject.SetActive(false);
            boltBullets.Enqueue(boltBullet);
        }
    }

    public TrapData GetData(int id)
    {
        if (trap_Data.Count <= id)
            return null;
        return trap_Data[id];
    }

    public InstallType GetTrapType(int id)
    {
        if (trap_Data.Count <= id)
            return 0;
        return trap_Data[id].trap_type;
    }

    public int GetTrapCost(int id)
    {
        if (trap_Data.Count <= id)
            return 0;
        return trap_Data[id].trap_cost;
    }
    public GameObject GetTrapObject(int id)
    {
        if (trap_Prefabs.Count <= id)
            return null;
        return trap_Prefabs[id];
    }
    public Sprite GetTrapSprite(int id)
    {
        if (trap_Sprite.Count <= id)
            return null;
        return trap_Sprite[id];
    }

    #region ���� �Ѿ� ������Ʈ Ǯ��
    // DartTrap
    public DartBullet GetDartBullet()
    {
        if(dartBullets.Count == 0)
        {
            DartBullet dartBullet = Instantiate(dartBullet_prefab, this.transform).GetComponent<DartBullet>();
            dartBullet.gameObject.SetActive(false);
            dartBullets.Enqueue(dartBullet);
        }

        DartBullet bullet = dartBullets.Peek();
        bullet.gameObject.SetActive(true);
        dartBullets.Dequeue();
        return bullet;
    }

    public void ReturnDartBullet(DartBullet bullet)
    {
        bullet.gameObject.SetActive(false);         // ������Ʈ ����
        bullet.transform.position = Vector3.zero;   // ��ġ�ʱ�ȭ
        dartBullets.Enqueue(bullet);                // queue ����
    }
    

    // BoltTrap
    public BoltBullet GetBoltBullet()
    {
        if(boltBullets.Count == 0)
        {
            BoltBullet boltBullet = Instantiate(boltBullet_prefab, this.transform).GetComponent<BoltBullet>();
            boltBullet.gameObject.SetActive(false);
            boltBullets.Enqueue(boltBullet);
        }

        BoltBullet bullet = boltBullets.Peek();
        bullet.gameObject.SetActive(true);
        boltBullets.Dequeue();
        return bullet;
    }
    public void ReturnBoltBullet(BoltBullet bullet)
    {
        bullet.gameObject.SetActive(false);         // ������Ʈ ����
        bullet.transform.position = Vector3.zero;   // ��ġ�ʱ�ȭ
        boltBullets.Enqueue(bullet);                // queue ����
    }
    #endregion

    public void DestoryTrap()
    {
        for (int i = 0; i < trap_transform.childCount; i++)
        {
            Destroy(trap_transform.GetChild(i).gameObject);
        }
    }
}
