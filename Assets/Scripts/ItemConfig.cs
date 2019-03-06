using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        HealthPotionItem healthPotionItem = new HealthPotionItem(1, "血瓶", "加血的");

        itemDict.Add(healthPotionItem.CfgId, healthPotionItem);
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
