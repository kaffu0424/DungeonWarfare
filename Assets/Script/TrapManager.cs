using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : Singleton<TrapManager>
{
    private List<TrapData> trap_Data;
    [SerializeField] private List<GameObject> trap_Prefabs;
    [SerializeField] private List<Sprite> trap_Sprite;
    protected override void InitManager()
    {
        trap_Data = new List<TrapData>();
        trap_Data.Add(new TrapData(0, "DartTrap", 2, 0.65f, 500, 9999, 3, InstallType.WALL));
        trap_Data.Add(new TrapData(1, "SpikeTrap", 3, 2.0f, 650, 9999, 0, InstallType.GROUND));
        trap_Data.Add(new TrapData(2, "BoltTrap", 6, 3.5f, 700, 9999, 3, InstallType.WALL));
        trap_Data.Add(new TrapData(3, "Barricade", 0, 0.0f, 550, 3, 0, InstallType.GROUND));
        trap_Data.Add(new TrapData(4, "SlimeTrap", 0, 0.0f, 300, 9999, 0, InstallType.GROUND));
        trap_Data.Add(new TrapData(5, "PushTrap", 2, 5.0f, 500, 9999, 1, InstallType.WALL));
        trap_Data.Add(new TrapData(6, "SpearTrap", 3, 4.0f, 200, 9999, 4, InstallType.WALL));
    }

    public TrapData GetData(int id)
    {
        if (trap_Data.Count <= id)
            return null;
        return trap_Data[id];
    }

    public InstallType GetTrapType(int id)
    {
        if (trap_Data.Count <= id)
            return 0;
        return trap_Data[id].trap_type;
    }

    public int GetTrapCost(int id)
    {
        if (trap_Data.Count <= id)
            return 0;
        return trap_Data[id].trap_cost;
    }
    public GameObject GetTrapObject(int id)
    {
        if (trap_Prefabs.Count <= id)
            return null;
        return trap_Prefabs[id];
    }
    public Sprite GetTrapSprite(int id)
    {
        if (trap_Sprite.Count <= id)
            return null;
        return trap_Sprite[id];
    }
}
