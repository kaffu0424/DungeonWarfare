using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("unity")]
    [SerializeField] private TextMeshProUGUI gold_Text;

    [SerializeField] private GameObject lobby_UI;
    [SerializeField] private GameObject game_UI;

    [SerializeField] private GameObject stage_popup;
    [SerializeField] private TextMeshProUGUI stage_name;

    [SerializeField] private Button wave_Button;

    private int selectLevel;
    protected override void InitManager()
    {
        wave_Button.gameObject.SetActive(false);
        lobby_UI.gameObject.SetActive(true);
        game_UI.gameObject.SetActive(false);

        stage_popup.gameObject.SetActive(false);
    }

    public void UpdateGold(int value)
    {
        gold_Text.text = value.ToString();
    }

    public void SelectStage(int stageLevel)
    {
        selectLevel = stageLevel;
        stage_name.text = "Stage " + (selectLevel + 1);
        StagePopupOn();
    }

    public void WaveStart()
    {
        wave_Button.gameObject.SetActive(false);
        GameManager.Instance.WaveStart();
    }

    public void StagePopupOn()
    {
        stage_popup.gameObject.SetActive(true);
    }
    public void StagePopupOff()
    {
        stage_popup.gameObject.SetActive(false);
    }

    public void ConfirmStage()
    {
        wave_Button.gameObject.SetActive(true);
        game_UI.SetActive(true);
        lobby_UI.SetActive(false);

        GameManager.Instance.StageStart(selectLevel);
    }
    public void CancleStage()
    {
        StagePopupOff();
    }

}
