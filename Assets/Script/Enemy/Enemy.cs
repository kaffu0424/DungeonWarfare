using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    [SerializeField] private Vector2 target;

    [SerializeField] private GameObject enemyObject;
    [SerializeField] private Transform enemy_hpbar;

    private bool onDead;

    private float maxHP;
    private float beforeSpeed;
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

        maxHP = data.hp;
        beforeSpeed = enemyData.moveSpped;
    }

    public void ResetEnemy(int _hp)
    {
        enemyData.hp = _hp;
        enemyData.moveSpped = beforeSpeed;
    }

    public EnemyData GetData()
    {
        return enemyData;
    }


    public void ResetSpeed()
    {
        enemyData.moveSpped = beforeSpeed;
    }
    public void SpeedDebuff(float amount)
    {
        enemyData.moveSpped *= amount;
    }

    public void GetDamage(int damage)
    {
        enemyData.hp -= damage;
        SoundManager.Instance.EnemyHitSFX();
        enemy_hpbar.localScale = new Vector3(1, enemyData.hp / maxHP , 1);
        if (enemyData.hp <= 0)
        {
            SoundManager.Instance.GetGoldSFX();
            GameManager.Instance.UseGold(enemyData.reward);
            GameManager.Instance.GetExp(enemyData.exp);
            Finished();
        }
    }

    public void Finished()
    {
        StopAllCoroutines();
        enemy_hpbar.localScale = Vector3.one; // HP바 초기화
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
        enemyObject.transform.up = dir;
    }
}
