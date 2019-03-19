using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionItem : BaseItem
{

    public int amount = 10;

    public HealthPotionItem(int id, string name, string desc) : base(id, name, desc)
    {

    }

    public override void Use(PlayerCtrl playerCtrl)
    {
        playerCtrl.AddHealth(10);
    }
}
