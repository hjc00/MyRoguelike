using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public string icon { get; private set; }
    public int gold { get; private set; }
    public int addHp { get; private set; }
    public string prefabName { get; private set; }
    public int addMp { get; private set; }
    public int addOff { get; private set; }
    public int addDef { get; private set; }

    public BaseItem(int id, string name, string desc, string icon, int gold,
        string prefabName, int addHp, int addMp, int addOff, int addDef)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = desc;
        this.icon = icon;
        this.gold = gold;
        this.prefabName = prefabName;
        this.addHp = addHp;
        this.addMp = addMp;
        this.addOff = addOff;
        this.addDef = addDef;
    }

    public virtual void Use(PlayerCtrl playerCtrl)
    {
        if (playerCtrl == null || playerCtrl.RoleData == null)
            return;

        if (this.addHp != 0)
        {
            playerCtrl.AddHealth(this.addHp);
        }

        if (this.addMp != 0)
        {
            playerCtrl.UpdateMp(this.addMp);
        }

        if (this.addOff != 0)
        {
            playerCtrl.UpdateAtkPower(this.addOff);
        }

        if (this.addDef != 0)
        {
            playerCtrl.UpdateDefPower(this.addDef);
        }

    }
}
