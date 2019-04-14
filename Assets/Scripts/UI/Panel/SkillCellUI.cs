using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCellUI : MonoBehaviour
{

    public Image icon;
    public Text desc;
    public Text skillName;

    private int id;
    private SkillNpcPanel panel;

    public void SetData(int id, SkillNpcPanel skillNpcPanel)
    {

        Skill tempSkill = SkillConfig.Instance.GetSkillById(id);
        this.id = id;
        this.panel = skillNpcPanel;
        icon.sprite = Resources.Load(GameDefine.skillIconPath + tempSkill.icon, typeof(Sprite)) as Sprite;

        desc.text = tempSkill.Desc;
        skillName.text = tempSkill.Name;

        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        panel.selectSkillId = this.id;
        this.panel.ShowBuy();
    }
}
