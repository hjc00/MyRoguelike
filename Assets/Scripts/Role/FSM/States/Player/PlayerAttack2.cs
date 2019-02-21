using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : FsmBase
{

    Animator anim;

    PlayerCtrl playerCtrl;

    float timer = 0;
    float animationEndTime = 0;

    public PlayerAttack2(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;

        animationEndTime = GameDefine.attack2Length * 0.5f;
    }

    public override void OnEnter()
    {
        Debug.Log("attack 2 state");
        anim.SetTrigger("attack");
        timer = 0;
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
        timer += Time.deltaTime;


        if (Input.GetMouseButtonDown(0))
        {
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Attack3);
            return;
        }


        if (timer >= animationEndTime)
        {
            timer = 0;
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
            return;
        }
    }
}
