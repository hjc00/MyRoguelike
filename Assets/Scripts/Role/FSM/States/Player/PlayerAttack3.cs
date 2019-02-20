using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3 : FsmBase
{

    Animator anim;

    PlayerCtrl playerCtrl;

    float timer = 0;
    float animationEndTime = 0;

    public PlayerAttack3(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;

        animationEndTime = GameDefine.attack3Length * 0.5f;
    }

    public override void OnEnter()
    {
        Debug.Log("attack 3 state");
        anim.SetTrigger("attack");
    }


    public override void OnStay()
    {
        timer += Time.deltaTime;

        if (timer < animationEndTime)
        {
            timer += Time.deltaTime;
            return;
        }

        if (timer >= animationEndTime)
        {
            timer = 0;
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
            return;
        }

        // HandleInput();
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
