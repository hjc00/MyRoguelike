using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{

    private int gold;

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            EventCenter.Broadcast<int>(EventType.OnUpdateGold, gold);
            PlayerPrefs.SetInt("gold", gold);
        }
    }
}
