using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltTrap : Trap
{
    [Header("unity")]
    [SerializeField] private Transform shootPosition_1;
    [SerializeField] private Transform shootPosition_2;
    [SerializeField] private Transform shootPosition_3;
    private void Start()
    {
        TrapData data = TrapManager.Instance.GetData(2);
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
        trap_audio = GetComponent<AudioSource>();
    }

    protected override void Attack()
    {
        //BoltBullet bullet_1 = TrapManager.Instance.GetBoltBullet();
        //bullet_1.transform.position = shootPosition_1.position;
        //bullet_1.MoveBullet(this.transform.localEulerAngles, trap_data.trap_damage);

        BoltBullet bullet_2 = TrapManager.Instance.GetBoltBullet();
        bullet_2.transform.position = shootPosition_2.position;
        bullet_2.MoveBullet(this.transform.localEulerAngles, trap_data.trap_damage);

        //BoltBullet bullet_3 = TrapManager.Instance.GetBoltBullet();
        //bullet_3.transform.position = shootPosition_3.position;
        //bullet_3.MoveBullet(this.transform.localEulerAngles, trap_data.trap_damage);

        //해당 함정의 기능은 한방이 강력한 함정으로 수정
    }
}
