using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Desc { get; private set; }

    public BaseItem(int id, string name, string desc)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = desc;
    }

    public virtual void Use(PlayerCtrl playerCtrl)
    {

    }
}
