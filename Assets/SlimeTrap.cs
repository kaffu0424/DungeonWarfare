using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrap : Trap
{
    private float debuff_amount;

    private void Start()
    {
        TrapData data = TrapManager.Instance.GetData(3);
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

        canAttack = false;
        debuff_amount = 0.65f;
    }

    protected override void Attack()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.SpeedDebuff(debuff_amount);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.ResetSpeed();
        }
    }
}
