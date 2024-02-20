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
        // 1. 플레이어 레벨, exp , 스테이지 진행현황 저장 필요
        // 1. json 데이터 사용
        player_level = 1;
        player_exp = 0;
    }

    public void StageStart(int stageLevel)
    {
        // 현재 스테이지 외 나머지 스테이지 비활성화.
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
        // 코루틴끝 -> 웨이브 끝
        // 1. 결과창 보여주기
        // 2. 결과창에서 로비로 돌아갈수있도록 구현하기
        // 
        // 결과창에 표시해야할것
        // 1. 플레이어가 획득한 경험치
    }
}