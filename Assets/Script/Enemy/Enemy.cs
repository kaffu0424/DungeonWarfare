using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;           // 데이터

    [SerializeField] private Vector2 target;                // 다음 이동 위치

    [SerializeField] private GameObject enemyObject;        // enemy object
    [SerializeField] private Transform enemy_hpbar;         // HP bar

    private bool onDead;                                    // 사망여부

    private float maxHP;                                    // 최대체력
    private float beforeSpeed;                              // 기존속도

    // 오브젝트 생성될때 초기화 함수
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

    // 오브젝트 풀링 초기화 함수
    public void ResetEnemy(int _hp)
    {
        enemyData.hp = _hp;
        enemyData.moveSpped = beforeSpeed;
    }

    // 현재 Enemy의 데이터를 반환하는 함수
    public EnemyData GetData()
    {
        return enemyData;
    }

    // 디버프 풀렸을때
    public void ResetSpeed()
    {
        enemyData.moveSpped = beforeSpeed;
    }

    // 디버프 받았을때
    public void SpeedDebuff(float amount)
    {
        enemyData.moveSpped *= amount;
    }

    // 데미지 받았을때
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

    // 도착 했을때 또는 데미지를 받아 사망했을때 함수
    public void Finished()
    {
        StopAllCoroutines();
        enemy_hpbar.localScale = Vector3.one; // HP바 초기화
        EnemyManager.Instance.ReturnEnemy(this);
    }

    // A* 알고리즘에 의해 생성된 경로를 매개변수로 받아 이동하는 함수
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

    // 이동하는 방향을 바라보는 함수
    void RotateEnemy(Vector2 target)
    {
        Vector3 dir = new Vector3(transform.position.x - target.x, transform.position.y - target.y, 0f);
        enemyObject.transform.up = dir;
    }
}
