using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("unity")]
    [SerializeField] private TextMeshProUGUI gold_Text;

    [Header("info")]
    [SerializeField] private int player_level;
    [SerializeField] private int player_exp;
    [SerializeField] private int gold;

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

        
        StageStart(); // 테스트용  //스테이지 시작하는곳에서 호출하기@@
    }

    public void StageStart()
    {
        UpdateGold(1000 + (player_level * 50));
    }

    public void UpdateGold(int value)
    {
        p_gold += value;
        gold_Text.text = p_gold.ToString();
    }
}

/*
## TODO
DartTrap.cs  Attack 함수 완성
설치 방식 구체적으로 변경하기.
*/