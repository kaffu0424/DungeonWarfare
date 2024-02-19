using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("unity")]
    [SerializeField] private PlayerController playerController;

    [Header("info")]
    [SerializeField] private int player_level;
    [SerializeField] private int player_exp;
    [SerializeField] private int gold;
    [SerializeField] private int current_StageID;

    [SerializeField] public List<StageData> StageDatas;
    
    public int p_gold
    {
        get { return gold; }
        set { gold = value; }
    }

    protected override void InitManager()
    {
        // ���� �ؾ��Ұ� level, exp
        player_level = 1;
        player_exp = 0;

        current_StageID = 0;
        StageStart(current_StageID); // �׽�Ʈ��  //�������� �����ϴ°����� ȣ���ϱ�@@
    }

    public void StageStart(int stageLevel)
    {
        UseGold(1000 + (player_level * 50));

        if (stageLevel == -1)
            return;

        playerController.SetStageData(StageDatas[stageLevel]);
        EnemyManager.Instance.PathFinding();
    }

    public void UseGold(int value)
    {
        p_gold += value;
        UIManager.Instance.UpdateGold(p_gold);
    }
    public StageData GetStageData()
    {
        return StageDatas[current_StageID];
    }

    public IEnumerator WaveStart()
    {
        StageData currentStage = StageDatas[current_StageID];
        List<List<Node>> path = EnemyManager.Instance.path;

        int waveLength = currentStage.wave_data.Count;
        for (int i = 0; i < waveLength; i++)
        {
            List<Tuple<int, int, int>> waveData = currentStage.wave_data[i];

            for (int j = 0; j < waveData.Count; j++)
            {
                int enemyID = waveData[j].Item1;
                int enemyCount = waveData[j].Item2;
                int enemyStart = waveData[j].Item3;

                for (int v = 0; v < enemyCount; v++)
                {
                    Enemy enemy = EnemyManager.Instance.GetEnemy(enemyID);
                    enemy.Move(path[enemyStart]);
                    yield return new WaitForSeconds( 3/enemyCount );
                    // 50 -> 25�� 100 -> 50��
                }
            }
            yield return new WaitForSeconds(10);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(WaveStart()); 
        }
    }
    // ## TODO
    // 1. �������� ���� â �����
    // 2. �������� ����� ���̺� ���۹�ư �����
}