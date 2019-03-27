using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthUseEffect : BaseItemUseEffect
{

    

    public override void use(RoleData roleData, int amount)
    {
        roleData.Health += amount;
    }
}
