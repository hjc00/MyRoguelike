using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPersueState : FsmBase
{

    Animator anim;
    EnemyCtrl enemyCtrl;

    public GoblinPersueState(Animator anim, EnemyCtrl ctrl)
    {
        this.anim = anim;
        this.enemyCtrl = ctrl;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnStay()
    {
        HandleInput();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
}
