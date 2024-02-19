using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStageData : MonoBehaviour
{
    GameManager gameManager;
    void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.StageDatas = new List<StageData>();

        AddStage_0();
    }

    void AddStage_0()
    {
        #region stage_0
        // 8 ������� , 9 ��������
        int[,] stage_0 = new int[11, 15]
        {
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane  0
            { 1,1,1,1,1,1,1,1,1,1,1,9,9,9,1 }, // lane  1
            { 1,1,1,1,1,1,1,1,1,1,1,9,9,9,1 }, // lane  2
            { 1,1,1,1,0,0,0,1,1,1,1,9,9,9,1 }, // lane  3
            { 1,1,1,0,0,1,0,1,1,1,1,1,0,1,1 }, // lane  4
            { 1,1,1,0,0,1,1,1,1,1,0,1,0,1,1 }, // lane  5
            { 1,1,0,0,0,0,1,0,0,0,0,1,0,1,1 }, // lane  6
            { 8,0,0,0,0,0,0,0,1,0,0,0,0,0,1 }, // lane  7
            { 8,0,0,0,0,0,0,1,1,1,1,1,1,0,1 }, // lane  8
            { 1,1,0,0,1,1,1,1,8,0,0,0,0,0,1 }, // lane  9
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane 10
        };

        // �� ��������� ��������
        List<Tuple<Vector2Int, Vector2Int>> start_target = new List<Tuple<Vector2Int, Vector2Int>>()
        {
            new Tuple<Vector2Int,Vector2Int>(new Vector2Int(7,0) , new Vector2Int(2,12)),
            new Tuple<Vector2Int,Vector2Int>(new Vector2Int(8,0) , new Vector2Int(2,12)),
            new Tuple<Vector2Int,Vector2Int>(new Vector2Int(9,8) , new Vector2Int(2,12)),
        };

        // ���̺� ������ 
        // �ٸ� ������ �ʿ�@@
        List<List<Tuple<int, int, int>>> wave_data = new List<List<Tuple<int, int, int>>>
        {
            // wave 1
            new List<Tuple<int, int, int>>
            {
                new Tuple<int,int,int>(0,3,0),
                new Tuple<int,int,int>(1,3,0),
                new Tuple<int,int,int>(2,3,0),
                new Tuple<int,int,int>(3,3,0),
                new Tuple<int,int,int>(4,3,0),
                new Tuple<int,int,int>(5,3,0),
                //new Tuple<int,int,int>(0,30,0),
                //new Tuple<int,int,int>(3,10,0),
                //new Tuple<int,int,int>(1,5,0)
            },
            // wave 2
            new List<Tuple<int, int, int> >
            {
                //new Tuple<int,int,int>(0,50,2),
                //new Tuple<int,int,int>(3,20,0),
                //new Tuple<int,int,int>(1,10,2)
            }
        };

        // �߰�
        gameManager.StageDatas.Add(new StageData(stage_0, 11, 15, start_target,wave_data));
        #endregion
    }
}