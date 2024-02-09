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
            new EnemyData(0, "농민", "약하고 느립니다.", 5, 1, 1, 2, 5, 10),
            new EnemyData(1, "귀족", "현상금이 높습니다.", 5, 1, 1, 2, 100, 5),
            new EnemyData(2, "모험가", "빠릅니다.", 6, 1, 1, 3, 5, 10),
            new EnemyData(3, "전사", "처음 가해지는 피해를 방어합니다.", 10, 1, 1, 2, 5, 20),
            new EnemyData(4, "도둑", "매우 빠릅니다.", 6, 1, 1, 4, 10, 25),
            new EnemyData(5, "기사", "무겁고 느립니다.", 30, 4, 1, 1, 10, 50)
        };
    }
}
