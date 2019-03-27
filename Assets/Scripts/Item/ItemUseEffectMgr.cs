using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseEffectMgr
{

    private Dictionary<int, BaseItemUseEffect> allEffects;

    private static ItemUseEffectMgr instance;

    public static ItemUseEffectMgr Instance
    {
        get
        {
            if (instance == null)
                return new ItemUseEffectMgr();
            return instance;
        }
    }

    public ItemUseEffectMgr()
    {
        allEffects = new Dictionary<int, BaseItemUseEffect>();

    }

    private void LoadJson()
    {

    }



}
