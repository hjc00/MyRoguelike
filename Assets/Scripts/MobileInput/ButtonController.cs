using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
    public Button btnA;
    public Button btnB;
    public Button btnC;
    public Button btnAtk;

    public Button btnPause;

    private List<SkillBtnCtrl> skillBtnCtrls = new List<SkillBtnCtrl>();

    PlayerCtrl playerCtrl;

    TweenCallback onOpenPausePanel;

    private void Start()
    {

        btnAtk.onClick.AddListener(OnBtnAtkClick);
        btnPause.onClick.AddListener(OnBtnPauseClick);

        EventCenter.AddListener<int>(EventType.OnLearnSkill, SetSkillBtnData);

        playerCtrl = NpcManager.Instance.Player.gameObject.GetComponent<PlayerCtrl>();

        skillBtnCtrls.Add(btnA.GetComponent<SkillBtnCtrl>());
        skillBtnCtrls.Add(btnB.GetComponent<SkillBtnCtrl>());
        skillBtnCtrls.Add(btnC.GetComponent<SkillBtnCtrl>());


        onOpenPausePanel += PauseGame;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        onOpenPausePanel -= PauseGame;
    }

    private void OnBtnPauseClick()
    {
        UIManager.Instance.PopPanel(GameDefine.pausePanel, onOpenPausePanel);
    }

    private void OnBtnAtkClick()
    {
        playerCtrl.Attack();
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

    private void OnDestroy()
    {
        onOpenPausePanel -= PauseGame;
    }
}
