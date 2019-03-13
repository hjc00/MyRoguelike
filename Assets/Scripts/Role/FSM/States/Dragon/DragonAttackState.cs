using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackState : FsmBase
{

    Animator anim;
    DragonCtrl dragonCtrl;

    public DragonAttackState(Animator anim, DragonCtrl ctrl)
    {
        this.anim = anim;
        this.dragonCtrl = ctrl;
    }

    public override void OnEnter()
    {
        this.dragonCtrl.DisableCtrl();
    }

    public override void OnStay()
    {

    }

    public override void OnExit()
    {
        this.dragonCtrl.EnableCtrl();
    }

    public override void HandleInput()
    {

    }

}
