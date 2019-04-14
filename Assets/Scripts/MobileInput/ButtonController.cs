using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button btnA;
    public Button btnB;
    public Button btnC;
    public Button btnAtk;

    private List<SkillBtnCtrl> skillBtnCtrls = new List<SkillBtnCtrl>();

    PlayerCtrl playerCtrl;

    private void Start()
    {

        btnAtk.onClick.AddListener(OnBtnAtkClick);

        EventCenter.AddListener<int>(EventType.OnLearnSkill, SetSkillBtnData);

        playerCtrl = NpcManager.Instance.Player.gameObject.GetComponent<PlayerCtrl>();

        skillBtnCtrls.Add(btnA.GetComponent<SkillBtnCtrl>());
        skillBtnCtrls.Add(btnB.GetComponent<SkillBtnCtrl>());
        skillBtnCtrls.Add(btnC.GetComponent<SkillBtnCtrl>());
    }


    private void OnBtnAtkClick()
    {
        playerCtrl.transform.GetComponent<Animator>().SetTrigger("attack");
        // playerCtrl.DoRectDamage();
    }


    private void SetSkillBtnData(int skillId)
    {
  
        for (int i = 0; i < skillBtnCtrls.Count; i++)
        {
            if (skillBtnCtrls[i].SkillId == -1)
            {
                skillBtnCtrls[i].SetSkillData(skillId);
                break;
            }
        }
    }


}
