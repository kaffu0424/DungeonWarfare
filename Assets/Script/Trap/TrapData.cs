using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum InstallType
{
    GROUND,
    WALL,
    NONE
}
public class TrapData
{
    public int trap_id;
    public string trap_name;
    public int trap_damage;
    public float trap_delay;
    public int trap_cost;
    public int trap_hp;
    public int trap_range;
    public InstallType trap_type; // º® 1  ¹Ù´Ú 0
    public TrapData(int _id, string _name, int _dmg, float _delay, int _cost, int _hp, int _range, InstallType _type)
    {
        trap_id = _id;
        trap_name = _name;
        trap_damage = _dmg;
        trap_delay = _delay;
        trap_cost = _cost;
        trap_hp = _hp;
        trap_range = _range;
        trap_type = _type;
    }
}
