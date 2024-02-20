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
    [SerializeField] private List<GameObject> stages;

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
        // ## TODO
        // 1. �÷��̾� ����, exp , �������� ������Ȳ ���� �ʿ�
        // 1. json ������ ���
        player_level = 1;
        player_exp = 0;
    }

    public void StageStart(int stageLevel)
    {
        // ���� �������� �� ������ �������� ��Ȱ��ȭ.
        for(int i = 0; i < stages.Count; i++)
        {
            stages[i].SetActive(false);
            if ( i == stageLevel )
                stages[i].SetActive(true);
        }

        current_StageID = stageLevel;

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

    public void WaveStart()
    {
        StartCoroutine(WaveCoroutine());
    }

    IEnumerator WaveCoroutine()
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
                    yield return new WaitForSeconds( 0.5f );
                }
            }
            yield return new WaitForSeconds(4);
        }
       
        // ## TODO
        // �ڷ�ƾ�� -> ���̺� ��
        // 1. ���â �����ֱ�
        // 2. ���â���� �κ�� ���ư����ֵ��� �����ϱ�
        // 
        // ���â�� ǥ���ؾ��Ұ�
        // 1. �÷��̾ ȹ���� ����ġ
    }
}