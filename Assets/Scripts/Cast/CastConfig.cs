using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CastConfig
{

    private static Dictionary<int, Cast> castInfoDict = new Dictionary<int, Cast>();


    public static void LoadJson()
    {
        if (castInfoDict.Count > 0)
            return;

        TextAsset textAsset = Resources.Load<TextAsset>("Json/Cast");

        Cast[] casts = JsonConvert.DeserializeObject<Cast[]>(textAsset.text);

        for (int i = 0; i < casts.Length; i++)
        {
            castInfoDict.Add(casts[i].id, casts[i]);
        }

        Array.Clear(casts, 0, casts.Length);
        casts = null;

    }

    public static Cast GetCastById(int id)
    {
        Cast temp = null;
        castInfoDict.TryGetValue(id, out temp);
        if (temp == null)
        {
            Debug.LogError("castInfo不存在" + id);
        }
        return temp;
    }


}
