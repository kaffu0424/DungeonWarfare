using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected TrapData trap_data;

    [Header("Trap infomation")]
    [SerializeField] protected AudioSource trap_audio;
    [SerializeField] protected Animator trap_ani;
    [SerializeField] protected bool canAttack;
    
    public void InitializeTrap(int _id, string _name, int _dmg, float _delay, int _cost, int _hp, int _range, InstallType _type)
    {
        canAttack = true;

        trap_data = new TrapData(_id, _name, _dmg, _delay, _cost, _hp, _range, _type);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(canAttack)
        {
            if(collision.CompareTag("Enemy"))
            {
                canAttack = false;
                StartCoroutine(AttackDelay());
            }
        }
    }

    protected abstract void Attack();
    IEnumerator AttackDelay()
    {
        trap_ani.SetTrigger("Attack");

        yield return new WaitForSeconds(trap_data.trap_delay);
        canAttack = true;
    }

    public TrapData GetData()
    { 
        return trap_data;
    }

    public void AttackSound()
    {
        trap_audio.PlayOneShot(trap_audio.clip, SoundManager.Instance.GetSFXVolume());
    }
}
