using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DartTrap : Trap
{
    [Header("Unity")]
    [SerializeField] Transform shootPosition;
    private void Start()
    {
        TrapData data = TrapManager.Instance.GetData(0);
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
        DartBullet bullet = TrapManager.Instance.GetDartBullet();
        bullet.transform.position = shootPosition.position;
        bullet.MoveBullet(this.transform.localEulerAngles,trap_data.trap_damage);
    }
}
