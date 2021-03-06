﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;

public class ItemConfig : MonoBehaviour
{
    private static ItemConfig instanse;

    public static ItemConfig Instance
    {
        get
        {
            return instanse;
        }
    }

    private Dictionary<int, BaseItem> itemDict = new Dictionary<int, BaseItem>();

    private void Awake()
    {
        if (instanse != null)
            return;

        instanse = this;
        LoadJson();

        // Debug.Log(itemDict[1000].icon);
        // Debug.Log(itemDict[1000].gold);
    }

    private void LoadJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/item");

        BaseItem[] baseItems = JsonConvert.DeserializeObject<BaseItem[]>(textAsset.text);

        for (int i = 0; i < baseItems.Length; i++)
        {
            // Debug.Log(baseItems[i].addHp);
            itemDict.Add(baseItems[i].Id, baseItems[i]);
        }

        Array.Clear(baseItems, 0, baseItems.Length);
        baseItems = null;

    }

    public BaseItem GetItemByCfgId(int cfgId)
    {
        if (!this.itemDict.ContainsKey(cfgId))
        {
            Debug.Log("物品不存在！");
            return null;
        }

        return this.itemDict[cfgId];
    }

    public int GetItemCount()
    {
        return itemDict.Count;
    }

    public void CreateItemObj(Vector3 pos, int id)
    {
        GameObject itemObj = Instantiate(Resources.Load<GameObject>(GameDefine.itemPrefabPath + this.GetItemByCfgId(id).prefabName),
            pos, Quaternion.identity) as GameObject;
    }
}
