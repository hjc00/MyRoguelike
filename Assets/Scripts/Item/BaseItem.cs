using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem
{
    protected int cfgId;
    protected string name;
    protected string desc;

    public BaseItem(int cfgId, string name, string desc)
    {
        this.cfgId = cfgId;
        this.name = name;
        this.desc = desc;
    }

    public virtual void Use(PlayerCtrl playerCtrl)
    {
       
    }
}
