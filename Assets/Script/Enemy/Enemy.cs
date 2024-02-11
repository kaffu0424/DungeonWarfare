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
        Debug.Log(enemyData.hp); 
        Debug.Log(damage);
        if(enemyData.hp <= 0)
        {
            GameManager.Instance.p_gold += enemyData.reward;
            EnemyManager.Instance.ReturnEnemy(this);
        }
    }
}
