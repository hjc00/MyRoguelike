using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class SkillConfig
{
    private static Dictionary<int, Skill> skillDict = new Dictionary<int, Skill>();


    public static void LoadJson()
    {
        if (skillDict.Count > 0)
            return;
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Skill");

        Skill[] skills = JsonConvert.DeserializeObject<Skill[]>(textAsset.text);

        for (int i = 0; i < skills.Length; i++)
        {
            skillDict.Add(skills[i].Id, skills[i]);
        }

        Array.Clear(skills, 0, skills.Length);
        skills = null;

    }

    public static Skill GetSkillById(int id)
    {
        Skill temp = null;
        skillDict.TryGetValue(id, out temp);
        if (temp == null)
        {
            Debug.LogError("技能不存在");
        }
        return temp;
    }

    public static void UseSkill(int skillId, Transform user)
    {
        if (!skillDict.ContainsKey(skillId))
        {
            Debug.LogError("技能不存在");
            return;
        }

        skillDict[skillId].Use(user);
    }

    public static void UseSkill(int skillId, Transform user, Vector3 pos)
    {
        if (!skillDict.ContainsKey(skillId))
        {
            Debug.LogError("技能不存在");
            return;
        }

        skillDict[skillId].Use(user, pos);
    }
}

