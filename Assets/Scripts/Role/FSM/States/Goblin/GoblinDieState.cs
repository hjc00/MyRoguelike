using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDieState : FsmBase
{

    Animator anim;
    EnemyCtrl enemyCtrl;

    public GoblinDieState(Animator anim, EnemyCtrl ctrl)
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
