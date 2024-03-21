using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public struct VolumeData
{
    public float bgm;
    public float sfx;

    public VolumeData(float bgm, float sfx)
    {
        this.bgm = bgm;
        this.sfx = sfx;
    }
}

public struct PlayerData
{
    public int playerLevel;
    public int playerexp;
    public int stageProgress;

    public PlayerData(int level, int exp, int progress)
    {
        playerLevel = level;
        playerexp = exp;
        stageProgress = progress;
    }
}

public class SaveSystem : MonoBehaviour
{
    private string savePath => Application.persistentDataPath + "/saves/";
    private string volumeSaveFileName = "volumeData";
    private string playerSaveFileName = "playerData";

    #region �÷��̾� ������
    public void SavePlayerData(int level, int exp, int progress)
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string saveData = JsonUtility.ToJson(new PlayerData(level, exp, progress));     // Json ��ȯ
        string saveFilePath = savePath + playerSaveFileName + ".json";

        File.WriteAllText(saveFilePath, saveData);
        Debug.Log("Save Success: " + saveFilePath);
    }
    public PlayerData LoadPlayerData()
    {
        string saveFilePath = savePath + playerSaveFileName + ".json";
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("Failed File Load");
            return new PlayerData(1,0,0);
        }
        string saveFile = File.ReadAllText(saveFilePath);
        PlayerData saveData = JsonUtility.FromJson<PlayerData>(saveFile);
        return saveData;
    }
    #endregion

    #region ���� ������
    public void SaveVolumeData(float bgm, float sfx)
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string saveData = JsonUtility.ToJson(new VolumeData(bgm, sfx));     // Json ��ȯ
        string saveFilePath = savePath + volumeSaveFileName + ".json";

        File.WriteAllText(saveFilePath, saveData);
        Debug.Log("Save Success: " + saveFilePath);
    }
    // TODO:����ý��� �������ϱ�
    public VolumeData LoadVolumeData()
    {
        string saveFilePath = savePath + volumeSaveFileName + ".json";
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("Failed File Load");
            return new VolumeData(0.25f, 0.25f);
        }
        string saveFile = File.ReadAllText(saveFilePath);
        VolumeData saveData = JsonUtility.FromJson<VolumeData>(saveFile);
        return saveData;
    }
#endregion
}
