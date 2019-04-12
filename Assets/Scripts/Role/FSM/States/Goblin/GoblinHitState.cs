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
        enemyCtrl.transform.LookAt(NpcManager.Instance.Player);
        anim.ResetTrigger("hit");
    }

    public override void OnStay()
    {

    }

    public override void OnExit()
    {
        anim.ResetTrigger("hit");

    }

}
