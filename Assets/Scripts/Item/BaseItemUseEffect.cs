using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemUseEffect
{

    public int Id { get; private set; }

    public virtual void use(RoleData roleData, int amount)
    {

    }
}
