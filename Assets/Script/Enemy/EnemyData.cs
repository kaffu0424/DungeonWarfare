using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public int id;
    public string name;
    public string description;
    public int hp;
    public int damage;
    public float attackSpeed;
    public float moveSpped;
    public int reward;
    public int exp;

    public EnemyData(int _id, string _name, string _description ,int _hp, int _damage, float _atkSpeed, float _moveSpeed, int _reward, int _exp)
    {
        id = _id;
        name = _name;
        description = _description;
        hp = _hp;
        damage = _damage;
        attackSpeed = _atkSpeed;
        moveSpped = _moveSpeed;
        reward = _reward;
        exp = _exp;
    }
}
