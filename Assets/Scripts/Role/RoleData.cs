using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData
{

    int health = 100;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    int speed = 8;   //移动速度

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    int atkPower = 10;   //攻击力
    public int AtkPower
    {
        get { return atkPower; }
        set { atkPower = value; }
    }
}
