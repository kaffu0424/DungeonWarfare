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

    [SerializeField] private List<StageData> StageDatas;
    
    public int p_gold
    {
        get { return gold; }
        set { gold = value; }
    }

    protected override void InitManager()
    {
        // 저장 해야할것 level, exp
        player_level = 1;
        player_exp = 0;

        SetStageData();
        
        StageStart(0); // 테스트용  //스테이지 시작하는곳에서 호출하기@@
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
        // 타일맵에서 다음 타일의 정보를 확인할수있는 방법이 필요함
        // 2차원 배열에 맵 정보를 담는 방법밖에 생각이 안난다..
        StageDatas = new List<StageData>();

        int[,] stage0 = new int[11,15]
        {
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane1
            { 1,1,1,1,1,1,1,1,1,1,1,9,9,9,1 }, // lane2
            { 1,1,1,1,1,1,1,1,1,1,1,9,9,9,1 }, // lane3
            { 1,1,1,1,0,0,0,1,1,1,1,9,9,9,1 }, // lane4
            { 1,1,1,0,0,1,0,1,1,1,1,1,0,1,1 }, // lane5
            { 1,1,1,0,0,1,1,1,1,1,0,1,0,1,1 }, // lane6
            { 1,1,0,0,0,0,1,0,0,0,0,1,0,1,1 }, // lane7
            { 8,0,0,0,0,0,0,0,1,0,0,0,0,0,1 }, // lane8
            { 8,0,0,0,0,0,0,1,1,1,1,1,1,0,1 }, // lane9
            { 1,1,0,0,1,1,1,1,8,0,0,0,0,0,1 }, // lane10
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane11
        };
        StageDatas.Add(new StageData(stage0,11,15));
    }
}