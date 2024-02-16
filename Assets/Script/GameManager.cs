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

    [SerializeField] private List<StageData> StageDatas;
    
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

        SetStageData();
        
        current_StageID = 0;
        StageStart(current_StageID); // �׽�Ʈ��  //�������� �����ϴ°����� ȣ���ϱ�@@
    }

    public void WaveStart()
    {
        // ��� ���̺� ���µ� �ɸ��½ð� 30��
        // ���� ���̺� �غ� �ð� 10��
        StageData data = StageDatas[current_StageID];
        WaveData wave = data.wave_data;

        // ##TODO
        // ���� �������ֱ�@!@
    }

    public void StageStart(int stageLevel)
    {
        UseGold(1000 + (player_level * 50));

        if (stageLevel == -1)
            return;

        playerController.SetStageData(StageDatas[stageLevel]);
    }

    public void UseGold(int value)
    {
        p_gold += value;
        UIManager.Instance.UpdateGold(p_gold);
    }

    void SetStageData()
    {
        // 8 : ������� , 9 : �������� 
        StageDatas = new List<StageData>();

        #region stage_0
        int[,] stage_0 = new int[11,15]
        {
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane0
            { 1,1,1,1,1,1,1,1,1,1,1,9,9,9,1 }, // lane1
            { 1,1,1,1,1,1,1,1,1,1,1,9,9,9,1 }, // lane2
            { 1,1,1,1,0,0,0,1,1,1,1,9,9,9,1 }, // lane3
            { 1,1,1,0,0,1,0,1,1,1,1,1,0,1,1 }, // lane4
            { 1,1,1,0,0,1,1,1,1,1,0,1,0,1,1 }, // lane5
            { 1,1,0,0,0,0,1,0,0,0,0,1,0,1,1 }, // lane6
            { 8,0,0,0,0,0,0,0,1,0,0,0,0,0,1 }, // lane7
            { 8,0,0,0,0,0,0,1,1,1,1,1,1,0,1 }, // lane8
            { 1,1,0,0,1,1,1,1,8,0,0,0,0,0,1 }, // lane9
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane10
        };
        List<Vector2Int> start = new List<Vector2Int>()
        {
            new Vector2Int(7,0),
            new Vector2Int(8,0),
            new Vector2Int(9,8)
        };
        List<Wave> wave = new List<Wave>() { new Wave(
                    new List<int> { 0 },    // enemy ID
                    new List<int> { 10 }    // enemy count
                    )};
        StageDatas.Add(new StageData(stage_0,11,15,start, new Vector2Int(2,12), new WaveData(wave)));
        #endregion

        #region stage_1
        // stage_0 �״�� �����ؿ���@@
        #endregion
    }

    public StageData GetStageData()
    {
        return StageDatas[current_StageID];
    }
}