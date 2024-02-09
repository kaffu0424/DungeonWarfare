using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("unity")]
    [SerializeField] private GameObject enemyObjects;



    private List<EnemyData> enemy_Data;
    protected override void InitManager()
    {
        enemy_Data = new List<EnemyData>
        {
            new EnemyData(0, "���", "���ϰ� �����ϴ�.", 5, 1, 1, 2, 5, 10),
            new EnemyData(1, "����", "������� �����ϴ�.", 5, 1, 1, 2, 100, 5),
            new EnemyData(2, "���谡", "�����ϴ�.", 6, 1, 1, 3, 5, 10),
            new EnemyData(3, "����", "ó�� �������� ���ظ� ����մϴ�.", 10, 1, 1, 2, 5, 20),
            new EnemyData(4, "����", "�ſ� �����ϴ�.", 6, 1, 1, 4, 10, 25),
            new EnemyData(5, "���", "���̰� �����ϴ�.", 30, 4, 1, 1, 10, 50)
        };
    }
}
