using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public int addHp { get; private set; }
    public int prefabName { get; private set; }
    public int addMp { get; private set; }
    public int addOff { get; private set; }
    public int addDef { get; private set; }

    public BaseItem(int id, string name, string desc, string prefabName, int addHp, int addMp, int addOff, int addDef)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = desc;
        this.addHp = addHp;
        this.addMp = addMp;
        this.addOff = addOff;
        this.addDef = addDef;
    }

    public virtual void Use(PlayerCtrl playerCtrl)
    {

    }
}
