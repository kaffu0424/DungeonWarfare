using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;           // ������

    [SerializeField] private Vector2 target;                // ���� �̵� ��ġ

    [SerializeField] private GameObject enemyObject;        // enemy object
    [SerializeField] private Transform enemy_hpbar;         // HP bar

    private bool onDead;                                    // �������

    private float maxHP;                                    // �ִ�ü��
    private float beforeSpeed;                              // �����ӵ�

    // ������Ʈ �����ɶ� �ʱ�ȭ �Լ�
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

    // ������Ʈ Ǯ�� �ʱ�ȭ �Լ�
    public void ResetEnemy(int _hp)
    {
        enemyData.hp = _hp;
        enemyData.moveSpped = beforeSpeed;
    }

    // ���� Enemy�� �����͸� ��ȯ�ϴ� �Լ�
    public EnemyData GetData()
    {
        return enemyData;
    }

    // ����� Ǯ������
    public void ResetSpeed()
    {
        enemyData.moveSpped = beforeSpeed;
    }

    // ����� �޾�����
    public void SpeedDebuff(float amount)
    {
        enemyData.moveSpped *= amount;
    }

    // ������ �޾�����
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

    // ���� ������ �Ǵ� �������� �޾� ��������� �Լ�
    public void Finished()
    {
        StopAllCoroutines();
        enemy_hpbar.localScale = Vector3.one; // HP�� �ʱ�ȭ
        EnemyManager.Instance.ReturnEnemy(this);
    }

    // A* �˰��� ���� ������ ��θ� �Ű������� �޾� �̵��ϴ� �Լ�
    public void Move(List<Node> path)
    {
        StopAllCoroutines();                                            // �ڷ�ƾ ������ �ʱ�ȭ
        this.transform.position = new Vector2(path[0].x, path[0].y);    // ������ġ��
        target = new Vector2(path[1].x, path[1].y);                     // ���� ��ǥ
        StartCoroutine(StartMove(path));                                // ��� �̵� ����
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

    // �̵��ϴ� ������ �ٶ󺸴� �Լ�
    void RotateEnemy(Vector2 target)
    {
        Vector3 dir = new Vector3(transform.position.x - target.x, transform.position.y - target.y, 0f);
        enemyObject.transform.up = dir;
    }
}
