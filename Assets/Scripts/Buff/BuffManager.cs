using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class BuffManager
{

    private static Dictionary<int, Buff> buffdict = new Dictionary<int, Buff>();

    public static void LoadJson()
    {

        if (buffdict.Count > 0)
            return;

        TextAsset textAsset = Resources.Load<TextAsset>("Json/Buff");

        Buff[] buffs = JsonConvert.DeserializeObject<Buff[]>(textAsset.text);

        for (int i = 0; i < buffs.Length; i++)
        {
            buffdict.Add(buffs[i].id, buffs[i]);
        }

        Array.Clear(buffs, 0, buffs.Length);
        buffs = null;
    }

    public static Buff GetBuffById(int id)
    {
        Buff temp = null;
        buffdict.TryGetValue(id, out temp);
        if (temp == null)
        {
            Debug.Log("buff 不存在！");
        }
        return temp;
    }

    public static void AddBuff(RoleData roleData, ActorBuff actorBuff, Buff buff)
    {
        actorBuff.AddBuff(roleData, buff);
    }

    public static void AddBuffs(RoleData roleData, ActorBuff actorBuff, List<Buff> buffs)
    {
     
        for (int i = 0; i < buffs.Count; i++)
        {
        
            actorBuff.AddBuff(roleData, buffs[i]);
        }

    }
}
