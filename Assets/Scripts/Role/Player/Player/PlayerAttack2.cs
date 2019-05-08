using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : FsmBase
{

    Animator anim;

    PlayerCtrl playerCtrl;


    public PlayerAttack2(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        // Debug.Log("attack 2 state");
        SkillPerform.Instance.Forward(playerCtrl.transform, 0.1f, 0.5f, playerCtrl.RoleData.rectLength);

        AudioManager.Instance.PlayClip(GameDefine.attackSoundName);
    }

    public override void OnStay()
    {
        HandleInput();

    }

    public override void OnExit()
    {

    }

    public override void HandleInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
            return;
        }

    }
}
