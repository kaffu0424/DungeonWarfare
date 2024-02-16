using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData enemyData;

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
            EnemyManager.Instance.ReturnEnemy(this);
        }
    }

    public void StartMove()
    {
        // A* 알고리즘? 데이크스트라 알고리즘?
        // 1. 알고리즘 선택하기
        // 2. 선택한 알고리즘으로 출발지점 -> 도착지점까지 최단거리 이동하기
        // 3. 이동 데이터는 PlayerController 또는 GameManager에서 Stage 정보 가져오기.
        // 4. Manager에서 한번에 계산하고 동일한 경로로 이동시키기 ?
    }
}
