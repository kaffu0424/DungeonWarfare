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
        // 8 출발지점 , 9 도착지점
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

        // 각 출발지점의 도착지점
        List<Tuple<Vector2Int, Vector2Int>> start_target = new List<Tuple<Vector2Int, Vector2Int>>()
        {
            new Tuple<Vector2Int,Vector2Int>(new Vector2Int(7,0) , new Vector2Int(2,12)),
            new Tuple<Vector2Int,Vector2Int>(new Vector2Int(8,0) , new Vector2Int(2,12)),
            new Tuple<Vector2Int,Vector2Int>(new Vector2Int(9,8) , new Vector2Int(2,12)),
        };

        // 웨이브 데이터 
        List<List<Tuple<int, int, int>>> wave_data = new List<List<Tuple<int, int, int>>>
        {
            // wave 1
            new List<Tuple<int, int, int>>
            {
                new Tuple<int,int,int>(0,15,0),
                new Tuple<int,int,int>(0,15,1),
                new Tuple<int,int,int>(1,3,0),
                new Tuple<int,int,int>(1,2,1)
            },
            //// wave 2
            //new List<Tuple<int, int, int> >
            //{
            //    new Tuple<int,int,int>(0,20,0),
            //    new Tuple<int,int,int>(0,20,1),
            //    new Tuple<int,int,int>(1,5,2),
            //    new Tuple<int,int,int>(1,5,1),
            //    new Tuple<int,int,int>(3,5,1)
            //},
            //// wave 3
            //new List<Tuple<int, int, int> >
            //{
            //    new Tuple<int,int,int>(0,25,0),
            //    new Tuple<int,int,int>(0,25,1),
            //    new Tuple<int,int,int>(3,10,1)
            //},

            //// wave 4
            //new List<Tuple<int, int, int> >
            //{
            //    new Tuple<int,int,int>(0,15,2),
            //    new Tuple<int,int,int>(0,15,1),
            //    new Tuple<int,int,int>(1,10,0),
            //    new Tuple<int,int,int>(3,10,1)
            //},
            ////wave 5
            //new List<Tuple<int, int, int> >
            //{
            //    new Tuple<int,int,int>(0,15,0),
            //    new Tuple<int,int,int>(0,15,1),
            //    new Tuple<int,int,int>(0,15,2),
            //    new Tuple<int,int,int>(2,5,1),
            //    new Tuple<int,int,int>(3,8,0),
            //    new Tuple<int,int,int>(2,8,1)
            //}
        };

        // 추가
        gameManager.StageDatas.Add(new StageData(stage_0, 11, 15, start_target,wave_data));
        #endregion

        #region stage_1
        // 8 출발지점 , 9 도착지점
        stage_0 = new int[20, 17]
        {
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane  0
            { 1,0,0,0,0,0,0,0,1,1,1,0,0,0,1,1,1 }, // lane  1
            { 1,0,1,1,1,1,1,0,1,1,1,0,1,0,1,1,1 }, // lane  2
            { 1,0,1,1,1,1,0,0,0,0,0,0,1,0,1,1,1 }, // lane  3
            { 1,0,0,0,0,0,0,1,1,1,0,1,1,0,1,1,1 }, // lane  4
            { 1,1,1,1,1,1,0,1,1,9,9,9,1,0,1,1,1 }, // lane  5
            { 1,0,0,0,0,1,0,0,0,9,9,9,0,0,0,1,1 }, // lane  6
            { 1,0,1,1,0,1,0,1,1,9,9,9,1,1,0,1,1 }, // lane  7
            { 1,0,1,1,0,0,0,1,1,1,0,1,1,1,0,1,1 }, // lane  8
            { 1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1 }, // lane  9
            { 1,0,1,0,0,0,0,0,0,1,0,1,1,1,0,1,1 }, // lane  10
            { 1,0,1,0,1,1,1,1,0,1,1,1,1,1,0,1,1 }, // lane  11
            { 1,0,0,0,1,0,0,0,0,0,0,0,0,1,0,1,1 }, // lane  12
            { 1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1 }, // lane  13
            { 1,0,0,0,0,1,0,0,0,1,1,1,0,1,0,1,1 }, // lane  14
            { 1,0,1,1,0,0,0,1,0,0,0,0,0,1,1,1,1 }, // lane  15
            { 1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane  16
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8 }, // lane  17
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8 }, // lane  18
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, // lane  19
        };

        // 각 출발지점의 도착지점
        start_target = new List<Tuple<Vector2Int, Vector2Int>>()
        {
            new Tuple<Vector2Int,Vector2Int>(new Vector2Int(18,16) , new Vector2Int(6,10)),
            new Tuple<Vector2Int,Vector2Int>(new Vector2Int(17,16) , new Vector2Int(6,10)),
        };

        // 웨이브 데이터 
        // 다른 저장방식 필요@@
        wave_data = new List<List<Tuple<int, int, int>>>
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
            },
            // wave 2
            new List<Tuple<int, int, int> >
            {

            }
        };

        // 추가
        gameManager.StageDatas.Add(new StageData(stage_0, 20, 17, start_target, wave_data));
        #endregion
    }
}
