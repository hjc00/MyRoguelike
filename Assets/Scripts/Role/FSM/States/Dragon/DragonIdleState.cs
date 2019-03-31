using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIdleState : FsmBase
{

    Animator anim;
    DragonCtrl dragonCtrl;

    public DragonIdleState(Animator anim, DragonCtrl ctrl)
    {
        this.anim = anim;
        this.dragonCtrl = ctrl;
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
