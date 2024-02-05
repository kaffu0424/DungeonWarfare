using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected TrapData trap_data;
    [SerializeField] protected bool canAttak;
    
    public void InitializeTrap(int _id, string _name, int _dmg, float _delay, int _cost, int _hp, int _range, int _type)
    {
        canAttak = true;

        trap_data = new TrapData(_id, _name, _dmg, _delay, _cost, _hp, _range, _type);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(canAttak)
        {
            if (collision.CompareTag("Enemy"))
                TrapFunction();
        }
    }

    protected virtual void TrapFunction()
    {
        canAttak = false;
        attack();
        StartCoroutine(attakDelay());
    }

    protected abstract void attack();
    IEnumerator attakDelay()
    {
        yield return new WaitForSeconds(trap_data.trap_delay);
        canAttak = true;
    }

}
