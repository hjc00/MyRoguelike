using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttack : FsmBase
{

    Animator anim;
    AnimatorStateInfo animatorInfo;
    LongRangeCtrl playerCtrl;


    public LongRangeAttack(Animator animator, LongRangeCtrl ctrl)
    {
        anim = animator;

        playerCtrl = ctrl;

    }

    public override void OnEnter()
    {

        SkillPerform.Instance.BeatBack(playerCtrl.transform, 0.1f, 0.1f);

    }


    public override void OnStay()
    {
        HandleInput();
    }

    public override void OnExit()
    {
        playerCtrl.SetAttack();
        anim.ResetTrigger("attack");
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
