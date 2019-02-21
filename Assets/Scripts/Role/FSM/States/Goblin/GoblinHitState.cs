using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinHitState : FsmBase
{
    Animator anim;
    EnemyCtrl enemyCtrl;

    public GoblinHitState(Animator anim, EnemyCtrl ctrl)
    {
        this.anim = anim;
        this.enemyCtrl = ctrl;
    }

    public override void OnEnter()
    {
        this.anim.SetTrigger("hit");
    }

    public override void OnStay()
    {

    }

    public override void OnExit()
    {
        Debug.Log("hit exit");
        enemyCtrl.FsmManager.ChangeState((int)GoblinAnimationEnum.Idle);
    }

}
