﻿using System.Collections;
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
        Debug.Log("attack 3 state");
        anim.SetTrigger("attack");
    }

    public override void OnStay()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
        }
    }

    public override void OnExit()
    {
        anim.ResetTrigger("attack");
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
}