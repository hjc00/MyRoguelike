using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionItem : BaseItem
{

    public int amount = 10;

    public HealthPotionItem(int id, string name, string desc, string prefabName, int addHp, int addMp, int addOff, int addDef) : base(id, name, desc, prefabName, addHp, addMp, addOff, addDef)
    {

    }

    public override void Use(PlayerCtrl playerCtrl)
    {
        playerCtrl.AddHealth(10);
    }
}
