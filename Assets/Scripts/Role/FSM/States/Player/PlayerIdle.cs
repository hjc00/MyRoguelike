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
        Debug.Log("idle state");
        base.OnEnter();
    }

    public override void OnStay()
    {
        HandleInput();
    }

    public override void OnExit()
    {
        base.OnExit();
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

            float y = Input.GetAxis("Vertical");


            if (x != 0 || y != 0)
            {
                playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Run);
            }

        }
    }
}
