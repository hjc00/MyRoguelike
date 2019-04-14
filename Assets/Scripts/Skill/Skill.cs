using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public string icon { get; private set; }


    public int range { get; private set; }
    public int needTarget { get; private set; }
    public int indicator { get; private set; }
    public int effectRange { get; private set; }

    public Skill(int id, string name, string desc, string icon, int range, int needTarget, int indicator, int effectRange)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = desc;
        this.icon = icon;

        this.range = range;
        this.needTarget = needTarget;
        this.indicator = indicator;
        this.effectRange = effectRange;
    }

    public void Use()
    {
        Debug.Log("use skill id " + this.Id);
    }
}
