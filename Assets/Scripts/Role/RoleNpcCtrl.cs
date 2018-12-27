using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcData
{
    private int health = 100;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
public class RoleNpcCtrl : RoleBaseCtrl
{


    private NpcData npcData;

    public NpcData NpcData
    {
        get { return npcData; }
    }

    void Awake()
    {
        npcData = new NpcData();
    }


    public void ReduceHealth(int amount)
    {
        NpcData.Health -= amount;
    }
}
