using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BGM
{
    LOBBY,
    GAME_SETUP,
    GAME_START,
    VICTORY,
    DEFEAT
}

public enum SFX
{
    CLICK,
    TRAPINSTALL,
    TRAPSELL,
    WAVESTART
}
public class SoundManager : Singleton<SoundManager>
{
    // object_BGM ( 배경음 )  BGM
    // object_SFX ( 버튼 클릭, 함정 설치 등등 UI와 연동되는 부분 ) SFX
    // 몬스터 사망, 트랩 공격  SFX ( object_SFX 와 따로 )

    [Header("unity")]
    [SerializeField] private AudioSource object_BGM;
    [SerializeField] private AudioSource object_SFX;       
    [SerializeField] private List<AudioClip> clips_BGM;
    [SerializeField] private List<AudioClip> clips_SFX;

    [SerializeField] private AudioSource object_Victory;
    [SerializeField] private AudioSource object_WaveStart;

    [SerializeField] private AudioSource object_EnemyDead;
    [SerializeField] private List<AudioClip> clips_EnemyDead;
    [SerializeField] private AudioSource object_GetGold;
    [SerializeField] private List<AudioClip> clips_GetGold;

    [SerializeField] private Slider slider_BGM;
    [SerializeField] private Slider slider_SFX;

    [Header("info")]
    [SerializeField] private float volume_BGM;
    [SerializeField] private float volume_SFX;
    protected override void InitManager()
    {
        // TODO:볼륨 저장 -> 저장된 값으로 초기화  추가하기

        volume_BGM = 0.25f;
        volume_SFX = 0.25f;

        object_BGM.volume = volume_BGM;
        object_SFX.volume = volume_SFX;

        
        slider_BGM.value = volume_BGM;
        slider_SFX.value = volume_SFX;

        slider_BGM.onValueChanged.AddListener(delegate { ChangeBGMVolume(); });
        slider_SFX.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }

    public void ChangeBGM(BGM _mode)
    {
        // Victory 브금 동작중 Lobby로 이동시 코루틴 멈춰서 빅토리 브금 안나오도록 수정
        StopAllCoroutines();

        object_BGM.clip = clips_BGM[(int)_mode]; // 클립 선택

        switch (_mode)
        {
            case BGM.LOBBY:
                object_BGM.loop = true;
                break;
            case BGM.GAME_SETUP:
                object_BGM.loop = true;
                break;
            case BGM.GAME_START:
                object_BGM.loop = true;
                break;
            case BGM.VICTORY:
                object_BGM.loop = false;
                StartCoroutine(VictoryBGM());
                break;
            case BGM.DEFEAT:
                object_BGM.loop = false;
                break;
        }

        object_BGM.Play();  // 클립 실행
    }

    IEnumerator VictoryBGM()
    {
        yield return new WaitForSeconds(6f);
        object_Victory.PlayOneShot(object_Victory.clip, volume_BGM);
    }

    public void ChangeSFX(SFX _mode)
    {
        object_SFX.Stop();
        object_SFX.clip = clips_SFX[(int)_mode];
        object_SFX.Play();
    }

    public void WaveStartSFX()
    {
        object_WaveStart.PlayOneShot(clips_SFX[(int)SFX.WAVESTART], volume_SFX);
    }

    public void EnemyHitSFX()
    {
        if (object_EnemyDead.isPlaying)
            return;

        object_EnemyDead.PlayOneShot(
            clips_EnemyDead[Random.Range(0,clips_EnemyDead.Count)], volume_SFX);
    }
    public void GetGoldSFX()
    {
        if (object_GetGold.isPlaying)
            return;
        
        object_GetGold.PlayOneShot(clips_GetGold[Random.Range(0,clips_GetGold.Count)], volume_SFX);
    }

    public float GetSFXVolume()
    {
        return volume_SFX;
    }

    #region 음량 조절
    void ChangeBGMVolume()
    {
        volume_BGM = slider_BGM.value;
        object_BGM.volume = volume_BGM;
    }
    void ChangeSFXVolume()
    {
        volume_SFX = slider_SFX.value;
    }
    #endregion
}
