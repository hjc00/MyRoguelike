using System.Collections;
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
        instanse = this;
        LoadJson();
     //   BaseItem healthPotionItem = new HealthPotionItem(1, "血瓶", "加血的");

      //  itemDict.Add(healthPotionItem.Id, healthPotionItem);
    }

    private void LoadJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/item");

        BaseItem[] baseItems = JsonConvert.DeserializeObject<BaseItem[]>(textAsset.text);

        for (int i = 0; i < baseItems.Length; i++)
        {
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

}
