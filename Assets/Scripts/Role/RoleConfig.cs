using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class RoleConfig
{
    public static Dictionary<int, RoleData> roleDataDict = new Dictionary<int, RoleData>();

    public static void LoadJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Role");

        RoleData[] roleDatas = JsonConvert.DeserializeObject<RoleData[]>(textAsset.text);

        for (int i = 0; i < roleDatas.Length; i++)
        {
            roleDataDict.Add(roleDatas[i].id, roleDatas[i]);
        }

        Array.Clear(roleDatas, 0, roleDatas.Length);
        roleDatas = null;
    }


    public static RoleData GetRoleDataById(int id)
    {
        RoleData temp = null;
        roleDataDict.TryGetValue(id, out temp);
        if (temp == null)
        {
            Debug.Log("角色配置不存在！");
        }
        return temp;
    }




}

