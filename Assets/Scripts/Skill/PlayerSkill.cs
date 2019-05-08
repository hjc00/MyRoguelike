using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{

    private List<Skill> ownSkills = new List<Skill>();

    private int maxSkillCnt = 3;

    private void Awake()
    {
        EventCenter.AddListener<int>(EventType.OnLearnSkill, AddSkill);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventType.OnLearnSkill, AddSkill);
    }

    public void AddSkill(int skillId)
    {
        if (ownSkills.Count >= maxSkillCnt)
        {
            UIManager.Instance.PopHint("你已学会3个技能！不能在学习");
            return;
        }
        Skill tempSkill = SkillConfig.GetSkillById(skillId);  
        ownSkills.Add(tempSkill);
        UIManager.Instance.PopHint("学习成功!");
    }



}
