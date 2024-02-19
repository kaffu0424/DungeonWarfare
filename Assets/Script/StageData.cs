using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public int[,] pos;
    public int size_y;
    public int size_x;
    public List<Tuple<Vector2Int, Vector2Int>> start_target; // { start , target }
    public List<List<Tuple<int, int, int>>> wave_data;
    // data[0~n] : 0~n ���̺�
    // data[1][0~m] : 0�� ���̺��� 0~m���� ����
    // data[1][0].item1 : 0��° ���̺��� 0��° ������ id
    // data[1][0].item2 : 0��° ���̺��� 0��° ������ ������
    // data[1][0].item3 : 0��° ���̺��� 0��° ������ ������ġ

    public StageData(int[,] _data,int _y, int _x, List<Tuple<Vector2Int, Vector2Int>> _start_target, List<List<Tuple<int, int, int>>> _wave_data)
    {
        pos = _data;

        this.size_y = _y;
        this.size_x = _x;

        start_target = _start_target;
        wave_data = _wave_data;
    }
}
