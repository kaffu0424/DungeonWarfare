using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DartTrap : Trap
{
    [SerializeField] Animator trap_ani;
    [SerializeField] GameObject bullet;
    private void Start()
    {
        InitializeTrap(0, "DartTrap", 2, 0.6f, 500, 9999, 3, 1);
        trap_ani = GetComponent<Animator>();
    }

    protected override void attack()
    {
        trap_ani.SetTrigger("Attack");
    }
}
