using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _y, int _x)
    {
        isWall = _isWall;
        y = _y;
        x = _x; 
    }
    public bool isWall;
    public Node ParentNode;

    public int x,y,G,H;
    // G : �������κ��� �̵��ߴ� �Ÿ�
    // H : ���� + ���� ��ֹ��� �����Ͽ� ��ǥ������ �Ÿ�
    public int F    // F : G + H
    { get { return G + H; } }
}

public class EnemyManager : Singleton<EnemyManager>
{
    int[] dy = { -1, -1, 0, 1, 1,  1,  0, -1 };
    int[] dx = { 0 ,  1, 1, 1, 0, -1, -1, -1 };

    [Header("unity")]
    [SerializeField] private List<GameObject> enemyPrefabs;

    private List<Queue<Enemy>> enemys;
    private List<EnemyData> enemy_Data;

    [SerializeField] private List<List<Node>> paths;
    public List<List<Node>> path
    { get { return paths; } }
    
    protected override void InitManager()
    {
        enemy_Data = new List<EnemyData>
        {
            new EnemyData(0, "���", "���ϰ� �����ϴ�.", 5, 1, 1, 2, 5, 10),
            new EnemyData(1, "����", "������� �����ϴ�.", 5, 1, 1, 2, 100, 5),
            new EnemyData(2, "���谡", "�����ϴ�.", 6, 1, 1, 3, 5, 10),
            new EnemyData(3, "����", "ó�� �������� ���ظ� ����մϴ�.", 10, 1, 1, 2, 5, 20),
            new EnemyData(4, "����", "�ſ� �����ϴ�.", 6, 1, 1, 4, 10, 25),
            new EnemyData(5, "���", "���̰� �����ϴ�.", 30, 4, 1, 1, 10, 50)
        };

        enemys = new List<Queue<Enemy>>();
        for(int i = 0; i < enemy_Data.Count; i++)
        {
            EnemyData data = enemy_Data[i];
            enemys.Add(new Queue<Enemy>());
            for(int j = 0; j < 1; j++)
            {
                Enemy enemy = Instantiate(enemyPrefabs[data.id], this.transform).GetComponent<Enemy>();
                enemy.InitEnemy(data);
                enemy.gameObject.SetActive(false);
                enemys[data.id].Enqueue(enemy);
            }
        }
    }

    public Enemy GetEnemy(int _id)
    {
        if (enemys[_id].Count == 0)
        {
            Enemy _enemy = Instantiate(enemyPrefabs[_id], this.transform).GetComponent<Enemy>();
            _enemy.InitEnemy(enemy_Data[_id]);
            _enemy.gameObject.SetActive(false);
            enemys[_id].Enqueue(_enemy);
        }
        Enemy enemy = enemys[_id].Peek();
        enemy.gameObject.SetActive(true);
        enemys[_id].Dequeue();
        return enemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        EnemyData data = enemy.GetData();
        enemy.ResetEnemy(enemy_Data[data.id].hp);
        enemy.gameObject.SetActive(false);
        enemys[data.id].Enqueue(enemy);
        // ## TODO
        // ���� ��� ����Ʈ �߰��ϱ� !
    }
    public void PathFinding()
    {
        paths = new List<List<Node>>();

        StageData stage = GameManager.Instance.GetStageData();

        Node[,] nodeArray = new Node[stage.size_y, stage.size_x];
        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();

        for(int i = 0; i < stage.size_y; i++)
        {
            for(int j = 0; j < stage.size_x; j++)
            {
                if (stage.pos[i, j] == 1)
                    nodeArray[i, j] = new Node(true, i, j);
                else
                    nodeArray[i, j] = new Node(false, i, j);
            }
        }

        foreach (Tuple<Vector2Int, Vector2Int> pos in stage.start_target)
        {
            openList.Clear(); closeList.Clear();

            Node startNode = nodeArray[pos.Item1.x, pos.Item1.y];
            Node targetNode = nodeArray[pos.Item2.x, pos.Item2.y];
            Node curNode;
            openList.Add(startNode);

            while(openList.Count > 0)
            {
                curNode = openList[0];
                for(int i = 1; i < openList.Count; i++)
                    if (openList[i].F <= curNode.F && openList[i].H < curNode.H)
                        curNode = openList[i];

                openList.Remove(curNode);
                closeList.Add(curNode);

                // ���� ��尡 ��������϶�
                if(curNode == targetNode)
                {
                    paths.Add(new List<Node>());
                    Node targetCurrent = targetNode;
                    while (targetCurrent != startNode)
                    {
                        paths[paths.Count - 1].Add(targetCurrent);
                        targetCurrent = targetCurrent.ParentNode;
                    }
                    paths[paths.Count - 1].Add(startNode);
                    paths[paths.Count - 1].Reverse();

                    continue;
                }

                OpenListAdd(true, curNode.y + 1, curNode.x + 1, ref curNode, ref openList, ref closeList,ref nodeArray, ref stage, ref targetNode);
                OpenListAdd(true, curNode.y - 1, curNode.x + 1, ref curNode, ref openList, ref closeList,ref nodeArray, ref stage, ref targetNode);
                OpenListAdd(true, curNode.y - 1, curNode.x - 1, ref curNode, ref openList, ref closeList,ref nodeArray, ref stage, ref targetNode);
                OpenListAdd(true, curNode.y + 1, curNode.x - 1, ref curNode, ref openList, ref closeList,ref nodeArray, ref stage, ref targetNode);

                OpenListAdd(false, curNode.y + 1, curNode.x, ref curNode, ref openList, ref closeList,ref nodeArray, ref stage, ref targetNode);
                OpenListAdd(false, curNode.y, curNode.x + 1, ref curNode, ref openList, ref closeList,ref nodeArray, ref stage, ref targetNode);
                OpenListAdd(false, curNode.y - 1, curNode.x, ref curNode, ref openList, ref closeList,ref nodeArray, ref stage, ref targetNode);
                OpenListAdd(false, curNode.y, curNode.x - 1, ref curNode, ref openList, ref closeList,ref nodeArray, ref stage, ref targetNode);
            }
        }


    }


    void OpenListAdd(bool diagonal, int nextY, int nextX, ref Node curNode, ref List<Node> openList, ref List<Node> closeList, ref Node[,] nodeArray,ref StageData stage, ref Node targetNode)
    {
        // ������ ���
        if (nextY < 0 || nextX < 0 || nextY >= stage.size_y || nextX >= stage.size_x)
            return;
        // �� / ���� ����Ʈ�� ����
        if (nodeArray[nextY, nextX].isWall || closeList.Contains(nodeArray[nextY,nextX]))
            return;

        if(diagonal)
        {
            // �� ���� �հ� ���°� ����
            if (nodeArray[curNode.y, nextX].isWall && nodeArray[nextY, curNode.x].isWall)
                return;
            // �ڳ� ����
            if (nodeArray[curNode.y, nextX].isWall || nodeArray[nextY, curNode.x].isWall)
                return;
        }

        Node nextNode = nodeArray[nextY, nextX];
        int cost = curNode.G + (curNode.x - nextX == 0 || curNode.y - nextY == 0 ? 10 : 14);
        
        if(cost < nextNode.G || !openList.Contains(nextNode))
        {
            nextNode.G = cost;
            nextNode.H = (Mathf.Abs(nextNode.x - targetNode.x) + Mathf.Abs(nextNode.y - targetNode.y)) * 10;
            nextNode.ParentNode = curNode;

            openList.Add(nextNode);
        }
    }

    private void OnDrawGizmos()
    {
        if (paths == null)
            return;

        if(paths.Count != 0)
        {
            foreach(List<Node> path in paths)
            {
                for(int i = 0; i < path.Count-1; i++)
                {
                    Vector2 from = new Vector2(path[i].x, path[i].y);
                    Vector2 to = new Vector2(path[i+1].x, path[i+1].y);
                    Gizmos.DrawLine(from, to);
                }
            }
        }
    }
}
