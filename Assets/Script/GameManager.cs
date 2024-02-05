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
        // ���� �ؾ��Ұ� level, exp
        player_level = 1;
        player_exp = 0;

        
        StageStart(); // �׽�Ʈ��  //�������� �����ϴ°����� ȣ���ϱ�@@
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
DartTrap.cs  Attack �Լ� �ϼ�
��ġ ��� ��ü������ �����ϱ�.
*/