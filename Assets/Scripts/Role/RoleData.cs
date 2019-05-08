using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public enum AtkType
{
    ShortRange = 1,
    LongRange = 2,
}

[Serializable]
public class RoleData
{
    public int id { get; set; }
    public string name { get; set; }

    public int hp { get; set; }


    public int mp { get; set; }
    public int atkPower { get; set; }
    public int defPower { get; set; }
    public int speed { get; set; }
    public int rectLength { get; set; }
    public int rectWidth { get; set; }
    public AtkType atkType { get; set; }
    public string cast { get; set; }

    [JsonIgnore]
    private static int instanceId = 0;
    [JsonIgnore]
    public int InstanceId { get; set; }
    [JsonIgnore]
    public RoleBaseCtrl ctrl;

    public RoleData()
    {

    }
    public RoleData(RoleData roleData)
    {
        this.InstanceId = instanceId++;
        this.id = roleData.id;
        this.name = roleData.name;
        this.hp = roleData.hp;
        this.mp = roleData.mp;
        this.atkPower = roleData.atkPower;
        this.defPower = roleData.defPower;
        this.speed = roleData.speed;
        this.rectLength = roleData.rectLength;
        this.rectWidth = roleData.rectWidth;
        this.atkType = roleData.atkType;
        this.cast = roleData.cast;
    }

    public void SetCtrl(RoleBaseCtrl ctrl)
    {
        this.ctrl = ctrl;
    }

   
}
