using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    [SerializeField] private BoxCollider2D hitbox;

    [Header("Unity")]
    [SerializeField] private LayerMask enemyMask;
    void Start()
    {
        TrapData data = TrapManager.Instance.GetData(1);
        InitializeTrap(
            data.trap_id,
            data.trap_name,
            data.trap_damage,
            data.trap_delay,
            data.trap_cost,
            data.trap_hp,
            data.trap_range,
            data.trap_type 
            );

        hitbox = GetComponent<BoxCollider2D>();
        trap_ani = GetComponent<Animator>();
    }

    protected override void Attack()
    {
        Collider2D[] enemyCollders = Physics2D.OverlapBoxAll(this.transform.position, hitbox.size, 0, enemyMask);
        for(int i = 0; i < enemyCollders.Length; i++)
        {
            try
            {
                enemyCollders[i].gameObject.GetComponent<Enemy>().GetDamage(trap_data.trap_damage);
            }
            catch
            { Debug.Log("error"); }
        }
    }
    // GetComponent를 사용하지않고 몬스터에게 데미지 넣어줄 방법 생각하기 ☆☆
}
