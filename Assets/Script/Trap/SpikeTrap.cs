using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
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

        trap_ani = GetComponent<Animator>();
    }

    protected override void Attack()
    {

    }
    // ## TODO
    // Spike ���� �����ϱ�.
    // ���� �����ϱ�.

    // GetComponent�� ��������ʰ� ���Ϳ��� ������ �־��� ��� �����ϱ� �١�
}
