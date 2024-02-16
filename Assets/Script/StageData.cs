using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<int> enemyID;  // ������ ������ ID
    public List<int> enemyCount;     // ������ ������ ����

    public Wave(List<int> _enemyID, List<int> _enemyCount)
    {
        this.enemyID = _enemyID;
        this.enemyCount = _enemyCount;
    }
}

[System.Serializable]
public class WaveData
{
    public int waveSize;    // �� ���̺� ��

    public List<Wave> waves;       // wave ������
    // waves[1~N] ���̺� ������

    public WaveData(List<Wave> _waves)
    {
        waveSize = _waves.Count;
        this.waves = _waves;
    }
}

[System.Serializable]
public class StageData
{
    public WaveData wave_data;
    public int[,] pos;
    public int size_y;
    public int size_x;
    public Vector2Int target_position;
    public List<Vector2Int> start_position;

    public StageData(int[,] _data,int _y, int _x, List<Vector2Int> _start, Vector2Int _target, WaveData _wave)
    {
        // ## TODO
        // �������ְ� �ڵ� �������ֱ�@@
        pos = _data;

        this.size_y = _y;
        this.size_x = _x;

        start_position = _start;
        target_position = _target;
        wave_data = _wave;
    }
}
