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
        // A* �˰���? ����ũ��Ʈ�� �˰���?
        // 1. �˰��� �����ϱ�
        // 2. ������ �˰������� ������� -> ������������ �ִܰŸ� �̵��ϱ�
        // 3. �̵� �����ʹ� PlayerController �Ǵ� GameManager���� Stage ���� ��������.
        // 4. Manager���� �ѹ��� ����ϰ� ������ ��η� �̵���Ű�� ?
    }
}
