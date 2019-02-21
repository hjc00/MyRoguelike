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

    }

    public override void OnStay()
    {

    }

    public override void OnExit()
    {

    }


}
