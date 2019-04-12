using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackState : FsmBase
{

    Animator anim;
    EnemyCtrl enemyCtrl;

    public GoblinAttackState(Animator anim, EnemyCtrl ctrl)
    {
        this.anim = anim;
        this.enemyCtrl = ctrl;
    }

    public override void OnEnter()
    {
        this.enemyCtrl.DisableCtrl();
    }

    public override void OnStay()
    {


    }

    public override void OnExit()
    {
        this.enemyCtrl.EnableCtrl();
    }

    public override void HandleInput()
    {
 
    }
}
