using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("unity")]
    [SerializeField] private Transform enemyPool;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private List<GameObject> stages;

    [Header("info")]
    [SerializeField] private int player_level;
    [SerializeField] private int player_exp;
    [SerializeField] private int stage_progress;
    [SerializeField] private int gold;
    [SerializeField] private int current_StageID;
    [SerializeField] private int current_HP;
    [SerializeField] private int current_exp;

    [SerializeField] public List<StageData> StageDatas;
    public int p_gold
    {
        get { return gold; }
        set { gold = value; }
    }
    public int p_progress
    {
        get { return stage_progress; }
        set { stage_progress = value; }
    }
    public int p_level
    {
        get { return player_level; }
        set { player_level = value; }
    }
    public int p_exp
    {
        get { return player_exp; }
        set { player_exp = value; }
    }
    public int c_stage
    {
        get { return current_StageID; }
        set { current_StageID = value; }
    }

    protected override void InitManager()
    {
        player_level = 1;
        player_exp = 0;
        stage_progress = 0;

        UIManager.Instance.OnLobby();
    }

    public void StageStart(int stageLevel)
    {
        current_HP = 20;
        current_exp = 0;
        UIManager.Instance.UpdateHP(current_HP);
        UIManager.Instance.UpdateExp(current_exp);

        // 현재 스테이지 외 나머지 스테이지 비활성화.
        for(int i = 0; i < stages.Count; i++)
        {
            stages[i].SetActive(false);
            if ( i == stageLevel )
                stages[i].SetActive(true);
        }

        current_StageID = stageLevel;

        p_gold = 2000 + (player_level * 50);
        UIManager.Instance.UpdateGold(p_gold);
        if (stageLevel == -1)
            return;

        playerController.SetStageData(StageDatas[stageLevel]);
        cameraController.SetCameraLimit(current_StageID);
        EnemyManager.Instance.PathFinding();
    }

    public void UseGold(int value)
    {
        p_gold += value;
        UIManager.Instance.UpdateGold(p_gold);
    }

    public void GetDamage()
    {
        current_HP--;
        UIManager.Instance.UpdateHP(current_HP);
        if(current_HP <= 0)
            DefenseFail();
    }

    public void GetExp(int value)
    {
        current_exp += value;
        UIManager.Instance.UpdateExp(current_exp);
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
        


        while(!CheckEnemy())
        {
            yield return new WaitForSeconds(3);
        }

        VictoryStage();
    }

    public void VictoryStage()
    {
        // 현재 스테이지 진행현황 +1
        if (current_StageID >= stage_progress)
            stage_progress++;

        TrapManager.Instance.DestoryTrap();


        float bonus = 1.0f + (current_HP / 100f);           // 체력 보너스
        int total = Mathf.RoundToInt(current_exp * bonus);  // 총 획득 경험치
        AddPlayerExp(total);
        SoundManager.Instance.ChangeBGM(BGM.VICTORY);
        UIManager.Instance.SuccessDefensePopup(current_exp, current_HP, bonus, total);
    }

    public void DefenseFail()
    {
        StopAllCoroutines();
        SoundManager.Instance.ChangeBGM(BGM.DEFEAT);
        TrapManager.Instance.DestoryTrap();
        EnemyManager.Instance.AllReturnEnemy();
        UIManager.Instance.FailDefensePopup();
    }

    public bool CheckEnemy()
    {
        for(int i = 0; i < enemyPool.childCount; i++)
        {
            if (enemyPool.GetChild(i).gameObject.activeSelf)
                return false;
        }
        return true;
    }

    public void AddPlayerExp(int value)
    {
        player_exp += value;
        while(player_exp >= (player_level * 1000))
        {
            player_exp -= (player_level * 1000);
            player_level++;
        }
    }

    public float GetExpAmout()
    {
        float maxExp = player_level * 1000;

        float amout = player_exp / maxExp;
        return amout;
    }
}