using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1 : FsmBase
{

    Animator anim;
    AnimatorStateInfo animatorInfo;
    PlayerCtrl playerCtrl;

    float timer = 0;
    float animationEndTime = 0;

    public PlayerAttack1(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;

        playerCtrl = ctrl;

        animationEndTime = GameDefine.attack1Length * 0.5f;
    }

    public override void OnEnter()
    {
        Debug.Log("attack 1 state");
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
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Attack2);
            return;
        }



        if (timer >= animationEndTime)
        {
            timer = 0;
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Idle);

        }
    }
}
