using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("unity")]
    [SerializeField] private TextMeshProUGUI gold_Text;
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
        UpdateGold(1000 + (player_level * 50));

        if (stageLevel == -1)
            return;

        playerController.SetStageData(StageDatas[stageLevel]);
    }

    public void UpdateGold(int value)
    {
        p_gold += value;
        gold_Text.text = p_gold.ToString();
    }

    void SetStageData()
    {
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
            { 1,0,0,0,0,0,0,0,1,0,0,0,0,0,1 }, // lane8
            { 1,0,0,0,0,0,0,1,1,1,1,1,1,0,1 }, // lane9
            { 1,1,0,0,1,1,1,1,1,0,0,0,0,0,1 }, // lane10
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane11
        };
        StageDatas.Add(new StageData(stage0,11,15));
    }
}

/*
## TODO
DartTrap.cs  Attack 함수 완성
설치 방식 구체적으로 변경하기.
*/