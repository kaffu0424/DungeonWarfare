using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("unity")]
    [SerializeField] private TextMeshProUGUI gold_Text;         // ��� ( ���� ��ġ ��ȭ )
    [SerializeField] private TextMeshProUGUI exp_Text;          // ����ġ ( ȹ�� ����ġ ( ���� ü�� )
    [SerializeField] private TextMeshProUGUI hp_Text;           // ü�� ( ���� ü�� )
    [SerializeField] private GameObject lobby_UI;               // �κ� UI
    [SerializeField] private GameObject game_UI;                // ���� UI ( �������� UI )
    [SerializeField] private GameObject stage_popup;            // �������� ���� �˾�
    [SerializeField] private TextMeshProUGUI stage_name;        // �������� ���� �˾� �ؽ�Ʈ
    [SerializeField] private Button wave_Button;                // �������� ���� ���� ���̺� ���� ��ư
    [SerializeField] private List<CanvasGroup> stage_select;    // ���ð����� ����������
    [SerializeField] private GameObject fail_UI;                // ���潺 ���� UI
    [SerializeField] private GameObject success_UI;             // ���潺 ���� UI
    [SerializeField] private TextMeshProUGUI success_XP;        // ���潺 ���� UI _ ȹ�� xp
    [SerializeField] private TextMeshProUGUI success_HP;        // ���潺 ���� UI _ ���� HP
    [SerializeField] private TextMeshProUGUI success_Bonus;     // ���潺 ���� UI _ HP��� ���ʽ� XP
    [SerializeField] private TextMeshProUGUI success_Total;     // ���潺 ���� UI _ ����ȹ���� ����ġ

    [SerializeField] private TextMeshProUGUI player_level;
    [SerializeField] private Image player_expGauge;
    [SerializeField] private Image player_expGaugeEff;

    [SerializeField] private GameObject mouserOverPopup;
    [SerializeField] private TextMeshProUGUI expMouseOverText;
    [SerializeField] private TextMeshProUGUI goldMouseOverText;
    [SerializeField] private TextMeshProUGUI levelMouseOverText;

    [SerializeField] private TextMeshProUGUI failSelectTrapText;
    private int selectLevel;

    protected override void InitManager()
    {
        
    }

    #region Game UI
    public void UpdateExp(int value)
    {
        exp_Text.text = value.ToString();
    }
    public void UpdateHP(int value)
    {
        hp_Text.text = value.ToString();
    }
    public void UpdateGold(int value)
    {
        gold_Text.text = value.ToString();
    }
    public void WaveStart()
    {
        wave_Button.gameObject.SetActive(false);
        GameManager.Instance.WaveStart();
    }

    public void FailSelectTrap()
    {
        if (failSelectTrapText.gameObject.activeSelf)
            return;

        failSelectTrapText.gameObject.SetActive(true);
        StartCoroutine(failTrap());
    }

    IEnumerator failTrap()
    {
        // ���� �ʱ�ȭ
        Color _color = failSelectTrapText.color;
        failSelectTrapText.color = new Color(_color.r, _color.g, _color.b, 1);

        yield return new WaitForSeconds(1f);
        while(failSelectTrapText.color.a > 0.01f)
        {
            failSelectTrapText.color =
                new Color(_color.r, _color.g, _color.b, failSelectTrapText.color.a - (Time.deltaTime / 1.5f));
            yield return null;
        }

        failSelectTrapText.gameObject.SetActive(false);
    }
    #endregion

    #region Stage Select Popup
    public void SelectStage(int stageLevel)
    {
        selectLevel = stageLevel;
        stage_name.text = "�������� " + (selectLevel + 1);
        StagePopupOn();
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
    #endregion

    #region Game END
    public void SuccessDefensePopup(int _xp, int _hp, float _bonus, int _total)
    {
        success_UI.SetActive(true);

        success_XP.text = _xp.ToString();
        success_HP.text = _hp.ToString();
        success_Bonus.text = "x " + _bonus.ToString();
        success_Total.text = _total.ToString();
    }
    public void FailDefensePopup()
    {
        fail_UI.SetActive(true);
    }
    #endregion

    #region Lobby UI
    public void StageProgress()
    {
        for (int i = 0; i < stage_select.Count; i++)
        {
            if (i <= GameManager.Instance.p_progress)
                stage_select[i].interactable = true;
            else
                stage_select[i].interactable = false;
        }
    }

    public void OnLobby()
    {
        lobby_UI.SetActive(true);

        wave_Button.gameObject.SetActive(false);
        stage_popup.SetActive(false);

        game_UI.SetActive(false);
        fail_UI.SetActive(false);
        success_UI.SetActive(false);

        StageProgress();

        ExpBarGaugeView();

        MouserOverLevel(false);
    }

    public void ExpBarGaugeView()
    {
        player_level.text = GameManager.Instance.p_level.ToString();

        float amout = GameManager.Instance.GetExpAmout();
        player_expGauge.fillAmount = amout;
        player_expGaugeEff.fillAmount = amout;
    }

    public void MouserOverLevel(bool bValue)
    {
        // ���콺 �ø��� ���� ���� �ֽ�ȭ
        if (bValue)
            LevelInformation();

        expMouseOverText.gameObject.SetActive(bValue);
        mouserOverPopup.SetActive(bValue);
    }

    void LevelInformation()
    {
        int playerLevel = GameManager.Instance.p_level;

        int currentExp = GameManager.Instance.p_exp;
        int maxExp = playerLevel * 1000;
        expMouseOverText.text = currentExp.ToString() + " / " + maxExp.ToString();
        levelMouseOverText.text = "���� " + playerLevel.ToString();
        goldMouseOverText.text =  "���� ��� \n+ " + (2000 + (playerLevel * 50)).ToString();
    }
    #endregion


    /*
     TODO:UI,����,����Ʈ
        UI
        1. ��ġ�� �������� ���� �߰� ( mouseOver )
        2. ���� ü�¹� �߰�

        ����
        1. ���� ���� �߰�
        2. ���� ���� �߰�
        3. ����� �߰�
        4. ��Ÿ ȿ���� �߰�

        ����Ʈ
        1. ���� ��� ����Ʈ �߰�
        2. ���� ���� ����Ʈ �߰�
    */
}
