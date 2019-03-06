using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionItem : BaseItem
{
    public int CfgId { get { return this.cfgId; } }
    public string Name { get { return this.name; } }
    public string Desc { get { return this.desc; } }

    public int amount = 10;

    public HealthPotionItem(int cfgId, string name, string desc) : base(cfgId, name, desc)
    {

    }

    public override void Use(PlayerCtrl playerCtrl)
    {
        playerCtrl.AddHealth(10);
    }
}
