﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData
{

    int health = 100;
    int maxHealth = 100;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health >= maxHealth)
                health = maxHealth;
        }
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

    public int DefPower { get; private set; }

    public RoleData(int health, int speed, int atkPower, int defPower)
    {
        this.health = health;
        this.speed = speed;
        this.atkPower = atkPower;
        this.DefPower = defPower;
    }

    public RoleData()
    {

    }
}
