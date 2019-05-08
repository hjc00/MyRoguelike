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

        this.enemyCtrl.RoleData.speed = 0;
       
    }

    public override void OnStay()
    {

    }

    public override void OnExit()
    {

    }

}
