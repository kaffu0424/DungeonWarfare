using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StageData
{
    public int[,] pos;
    public int size_y;
    public int size_x;

    public StageData(int[,] data,int y, int x)
    {
        pos = data;
        size_y = y;
        size_x = x;
    }
}
