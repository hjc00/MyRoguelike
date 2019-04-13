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

    public Skill(int id, string name, string desc, string icon)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = desc;
        this.icon = icon;
    }
}
