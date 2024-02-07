using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("unity")]
    [SerializeField] private TextMeshProUGUI gold_Text;

    protected override void InitManager()
    {
       
    }

    public void UpdateGold(int value)
    {
        gold_Text.text = value.ToString();
    }
}
