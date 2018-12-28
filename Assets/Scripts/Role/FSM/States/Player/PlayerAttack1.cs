using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1 : FsmBase
{

    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerAttack1(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        anim.SetTrigger("attack");
    }


    float timer = 0;
    public override void OnStay()
    {
        HandleInput();
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && !Input.GetMouseButtonDown(0))
        {
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Attack2);
        }
    }
}
