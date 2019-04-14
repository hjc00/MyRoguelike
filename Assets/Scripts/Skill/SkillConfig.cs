using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class SkillConfig
{
    private Dictionary<int, Skill> skillDict;

    private static SkillConfig instance;

    public static SkillConfig Instance
    {
        get
        {
            if (instance == null)
            {
                return new SkillConfig();
            }
            return instance;
        }
    }

    public SkillConfig()
    {
        skillDict = new Dictionary<int, Skill>();
        LoadJson();
    }

    private void LoadJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Skill");

        Skill[] skills = JsonConvert.DeserializeObject<Skill[]>(textAsset.text);

        for (int i = 0; i < skills.Length; i++)
        {
            skillDict.Add(skills[i].Id, skills[i]);
        }

        Array.Clear(skills, 0, skills.Length);
        skills = null;

    }

    public Skill GetSkillById(int id)
    {
        Skill temp = null;
        this.skillDict.TryGetValue(id, out temp);
        if (temp == null)
        {
            Debug.LogError("技能不存在");
        }
        return temp;
    }

    public void UseSkill(int skillId)
    {
        if (!this.skillDict.ContainsKey(skillId))
        {
            Debug.LogError("技能不存在");
            return;
        }

        skillDict[skillId].Use();
    }
}

