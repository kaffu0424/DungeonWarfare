using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("unity")]
    [SerializeField] private List<GameObject> enemyPrefabs;


    private List<Queue<Enemy>> enemys;
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

        enemys = new List<Queue<Enemy>>();
        for(int i = 0; i < enemy_Data.Count; i++)
        {
            EnemyData data = enemy_Data[i];
            enemys.Add(new Queue<Enemy>());
            for(int j = 0; j < 20; j++)
            {
                Enemy enemy = Instantiate(enemyPrefabs[data.id], this.transform).GetComponent<Enemy>();
                enemy.InitEnemy(data);
                enemy.gameObject.SetActive(false);
                enemys[data.id].Enqueue(enemy);
            }
        }

        Enemy testEnemy = GetEnemy(0);
        Instantiate(testEnemy);
    }

    public Enemy GetEnemy(int _id)
    {
        if (enemys[_id].Count == 0)
        {
            Enemy _enemy = Instantiate(enemyPrefabs[_id], this.transform).GetComponent<Enemy>();
            _enemy.InitEnemy(enemy_Data[_id]);
            _enemy.gameObject.SetActive(false);
            enemys[_id].Enqueue(_enemy);
        }
        Enemy enemy = enemys[_id].Peek();
        enemy.gameObject.SetActive(true);
        enemys[_id].Dequeue();
        return enemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        EnemyData data = enemy.GetData();
        enemy.ResetEnemy(enemy_Data[data.id].hp);
        enemy.gameObject.SetActive(false);
        enemys[data.id].Enqueue(enemy);
        // ## TODO
        // ���� ��� ����Ʈ �߰��ϱ� !
    }
}
