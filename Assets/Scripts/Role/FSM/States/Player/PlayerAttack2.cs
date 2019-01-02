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
        Debug.Log("attack 2 state");
        anim.SetTrigger("attack");
    }

    public override void OnStay()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && !Input.GetMouseButtonDown(0))
        {
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
        }
        else
        {
            HandleInput();
        }
    }

    public override void OnExit()
    {
       
    }

    public override void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Attack3);
        }
    }
}
