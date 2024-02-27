using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    [SerializeField] private Vector2 target;
    public void InitEnemy(EnemyData data)
    {
        enemyData = new EnemyData(
            data.id, 
            data.name, 
            data.description, 
            data.hp,
            data.damage, 
            data.attackSpeed, 
            data.moveSpped, 
            data.reward, 
            data.exp);
    }

    public void ResetEnemy(int _hp)
    {
        enemyData.hp = _hp;
    }

    public EnemyData GetData()
    {
        return enemyData;
    }

    public void GetDamage(int damage)
    {
        enemyData.hp -= damage;

        if(enemyData.hp <= 0)
        {
            GameManager.Instance.UseGold(enemyData.reward);
            GameManager.Instance.GetExp(enemyData.exp);
            Finished();
        }
    }

    public void Finished()
    {
        StopAllCoroutines();
        EnemyManager.Instance.ReturnEnemy(this);
    }

    public void Move(List<Node> path)
    {
        StopAllCoroutines();                                            // 코루틴 시작전 초기화
        this.transform.position = new Vector2(path[0].x, path[0].y);    // 시작위치로
        target = new Vector2(path[1].x, path[1].y);                     // 다음 목표
        StartCoroutine(StartMove(path));                                // 경로 이동 시작
    }

    public IEnumerator StartMove(List<Node> path)
    {
        int targetIdx = 1;
        while (true)
        {
            float moveSpeed = enemyData.moveSpped * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed);
            if(Vector2.Distance(transform.position, target) <= 0.3f)
            {
                if (targetIdx >= path.Count - 1)
                    break;

                targetIdx++;
                target = new Vector2(path[targetIdx].x, path[targetIdx].y);
            }
            RotateEnemy(target);

            yield return null;
        }

        GameManager.Instance.GetDamage();
        Finished();
    }

    void RotateEnemy(Vector2 target)
    {
        Vector3 dir = new Vector3(transform.position.x - target.x, transform.position.y - target.y, 0f);
        transform.up = dir;
    }
}
