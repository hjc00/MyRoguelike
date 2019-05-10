using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIdleState : FsmBase
{

    Animator anim;
    BossCtrl ctrl;

    public DragonIdleState(Animator anim, BossCtrl ctrl)
    {
        this.anim = anim;
        this.ctrl = ctrl;
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

    public override void HandleInput()
    {

    }
}
