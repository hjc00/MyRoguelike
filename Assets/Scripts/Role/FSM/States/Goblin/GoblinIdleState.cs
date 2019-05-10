using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinIdleState : FsmBase
{

    Animator anim;
    EnemyCtrl enemyCtrl;

    public GoblinIdleState(Animator anim, EnemyCtrl ctrl)
    {
        this.anim = anim;
        this.enemyCtrl = ctrl;
    }


    public override void OnEnter()
    {
        anim.ResetTrigger("hit");
    }

    public override void OnStay()
    {
        if (enemyCtrl.HasTarget())
        {
            enemyCtrl.ChangeToPersue();
        }
    }

    public override void OnExit()
    {

    }


}
