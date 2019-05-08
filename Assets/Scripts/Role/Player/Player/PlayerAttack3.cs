using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3 : FsmBase
{

    Animator anim;

    PlayerCtrl playerCtrl;


    public PlayerAttack3(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        // Debug.Log("attack 3 state");
        SkillPerform.Instance.Forward(playerCtrl.transform, 0.1f, 0.5f, playerCtrl.RoleData.rectLength);
        AudioManager.Instance.PlayClip(GameDefine.attackSoundName);
    }


    public override void OnStay()
    {

    }

    public override void OnExit()
    {
        anim.ResetTrigger("attack");
    }

    public override void HandleInput()
    {

    }
}
