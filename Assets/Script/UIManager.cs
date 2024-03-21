using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("unity")]
    [SerializeField] private TextMeshProUGUI gold_Text;         // 골드 ( 함정 설치 재화 )
    [SerializeField] private TextMeshProUGUI exp_Text;          // 경험치 ( 획득 경험치 ( 몬스터 체력 )
    [SerializeField] private TextMeshProUGUI hp_Text;           // 체력 ( 현재 체력 )
    [SerializeField] private GameObject lobby_UI;               // 로비 UI
    [SerializeField] private GameObject game_UI;                // 게임 UI ( 스테이지 UI )
    [SerializeField] private GameObject stage_popup;            // 스테이지 입장 팝업
    [SerializeField] private TextMeshProUGUI stage_name;        // 스테이지 입장 팝업 텍스트
    [SerializeField] private Button wave_Button;                // 스테이지 입장 이후 웨이브 시작 버튼
    [SerializeField] private List<CanvasGroup> stage_select;    // 선택가능한 스테이지들
    [SerializeField] private GameObject fail_UI;                // 디펜스 실패 UI
    [SerializeField] private GameObject success_UI;             // 디펜스 성공 UI
    [SerializeField] private TextMeshProUGUI success_XP;        // 디펜스 성공 UI _ 획득 xp
    [SerializeField] private TextMeshProUGUI success_HP;        // 디펜스 성공 UI _ 남은 HP
    [SerializeField] private TextMeshProUGUI success_Bonus;     // 디펜스 성공 UI _ HP비례 보너스 XP
    [SerializeField] private TextMeshProUGUI success_Total;     // 디펜스 성공 UI _ 최종획득한 경험치

    [SerializeField] private TextMeshProUGUI player_level;
    [SerializeField] private Image player_expGauge;
    [SerializeField] private Image player_expGaugeEff;

    [SerializeField] private GameObject mouserOverPopup;
    [SerializeField] private TextMeshProUGUI expMouseOverText;
    [SerializeField] private TextMeshProUGUI goldMouseOverText;
    [SerializeField] private TextMeshProUGUI levelMouseOverText;

    [SerializeField] private TextMeshProUGUI failSelectTrapText;

    [SerializeField] private GameObject informationUI;
    [SerializeField] private TextMeshProUGUI information_Name;
    [SerializeField] private TextMeshProUGUI information_info;
    [SerializeField] private TextMeshProUGUI information_Damage;
    [SerializeField] private TextMeshProUGUI information_Cooltime;

    [SerializeField] private GameObject trapSlotInformationUI;
    [SerializeField] private RectTransform trapSlotInformationUIRect;
    [SerializeField] private TextMeshProUGUI trapInformation_name;
    [SerializeField] private TextMeshProUGUI trapInformation_gold;

    [SerializeField] private GameObject settingUI;

    [SerializeField] private GameObject exitPopup;

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
        SoundManager.Instance.ChangeSFX(SFX.CLICK);
        SoundManager.Instance.WaveStartSFX();
        SoundManager.Instance.ChangeBGM(BGM.GAME_START);
        wave_Button.gameObject.SetActive(false);
        GameManager.Instance.WaveStart();
    }
    public void FailSelectTrap()
    {
        if (failSelectTrapText.gameObject.activeSelf)
            return;

        failSelectTrapText.gameObject.SetActive(true);
        StartCoroutine(FailTrap());
    }
    IEnumerator FailTrap()
    {
        // 투명도 초기화
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

    public void InformationUI(bool bvalue)
    {
        informationUI.SetActive(bvalue);
    }
    public void UpdateTrapInfomation(TrapData data)
    {
        information_Name.text = data.trap_name;
        information_info.text = "";
        information_Damage.text = "피해 : " + data.trap_damage.ToString();
        information_Cooltime.text = "쿨타임 : " + data.trap_delay.ToString();
    }
    public void UpdateEnemyInfomation(EnemyData data)
    {
        information_Name.text = data.name;
        information_info.text = data.description;
        information_Damage.text = "";
        information_Cooltime.text = "";
    }

    public void MouseOverTrapSlot(bool bValue, int _id, RectTransform transform)
    {
        if(bValue)
        {
            TrapData data = TrapManager.Instance.GetData(_id);
            trapInformation_name.text = data.trap_name;
            trapInformation_gold.text = data.trap_cost.ToString();

            trapSlotInformationUI.transform.SetParent(transform);

            trapSlotInformationUIRect.offsetMin = Vector2.zero;
            trapSlotInformationUIRect.offsetMax = Vector2.zero;
        }
        trapSlotInformationUI.gameObject.SetActive(bValue);
    }
    #endregion

    #region Stage Select Popup
    public void SelectStage(int stageLevel)
    {
        SoundManager.Instance.ChangeSFX(SFX.CLICK);

        selectLevel = stageLevel;
        stage_name.text = "스테이지 " + (selectLevel + 1);
        StagePopup(true);
    }
    public void StagePopup(bool bValue)
    {
        stage_popup.gameObject.SetActive(bValue);
    }
    public void ConfirmStage()
    {
        SoundManager.Instance.ChangeSFX(SFX.CLICK);

        wave_Button.gameObject.SetActive(true);
        game_UI.SetActive(true);
        lobby_UI.SetActive(false);

        GameManager.Instance.StageStart(selectLevel);
        SoundManager.Instance.ChangeBGM(BGM.GAME_SETUP);
    }
    public void CancleStage()
    {
        SoundManager.Instance.ChangeSFX(SFX.CLICK);

        StagePopup(false);
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
    public void ResultUIButton()
    {
        SoundManager.Instance.ChangeSFX(SFX.CLICK);
        OnLobby();
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
        SoundManager.Instance.ChangeBGM(BGM.LOBBY);

        lobby_UI.SetActive(true);

        wave_Button.gameObject.SetActive(false);
        stage_popup.SetActive(false);

        game_UI.SetActive(false);
        fail_UI.SetActive(false);
        success_UI.SetActive(false);

        StageProgress();                            // 스테이지 진행상황

        ExpBarGaugeView();                          // 레벨 / 경험치 게이지

        MouserOverLevel(false);                     // 레벨 마우스 Over UI

        InformationUI(false);                       // 오브젝트 정보 UI
                
        SettingUI(false);                           // Setting UI

        trapSlotInformationUI.gameObject.SetActive(false);
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
        // 마우스 올릴때 마다 정보 최신화
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
        levelMouseOverText.text = "레벨 " + playerLevel.ToString();
        goldMouseOverText.text =  "시작 골드 \n+ " + (4000 + (playerLevel * 200)).ToString();
    }
    #endregion

    #region Setting UI
    public void SettingUI(bool bValue)
    {
        // UI ON
        if (bValue) 
            Time.timeScale = 0;
        // UI OFF
        else
            Time.timeScale = 1;

        settingUI.SetActive(bValue);
    }

    public void ExitPopUp(bool bValue)
    {
        exitPopup.SetActive(bValue);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}