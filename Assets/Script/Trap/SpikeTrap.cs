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
    // Spike 함정 구현하기.
    // 몬스터 구현하기.

    // GetComponent를 사용하지않고 몬스터에게 데미지 넣어줄 방법 생각하기 ☆☆
}
