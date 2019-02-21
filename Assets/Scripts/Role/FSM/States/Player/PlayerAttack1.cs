using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1 : FsmBase
{

    Animator anim;
    AnimatorStateInfo animatorInfo;
    PlayerCtrl playerCtrl;


    public PlayerAttack1(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;

        playerCtrl = ctrl;

    }

    public override void OnEnter()
    {
        Debug.Log("attack 1 state");

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
