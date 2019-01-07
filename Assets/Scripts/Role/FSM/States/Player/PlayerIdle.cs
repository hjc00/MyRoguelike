using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : FsmBase
{
    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerIdle(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("idle state");
        anim.SetFloat("velocity", 0);
        anim.ResetTrigger("attack");
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
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Attack1);
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            float x = Input.GetAxis("Horizontal");

            float z = Input.GetAxis("Vertical");

            if (x != 0 || z != 0)
            {
                playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Run);
            }

        }
    }
}
