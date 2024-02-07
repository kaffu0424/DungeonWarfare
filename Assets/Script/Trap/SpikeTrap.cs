using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    [SerializeField] Animator trap_ani;

    private TrapData data;
    void Start()
    {
        data = TrapManager.Instance.GetData(1);
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

        // InitializeTrap(0, "SpikeTrap", 2, 0.6f, 500, 9999, 0, 0);

        trap_ani = GetComponent<Animator>();
    }

    protected override void attack()
    {
        throw new System.NotImplementedException();
    }

}
