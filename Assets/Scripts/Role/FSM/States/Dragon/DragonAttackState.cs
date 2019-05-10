using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackState : FsmBase
{

    Animator anim;
    BossCtrl bossCtrl;

    public DragonAttackState(Animator anim, BossCtrl ctrl)
    {
        this.anim = anim;
        this.bossCtrl = ctrl;
    }

    public override void OnEnter()
    {
        this.bossCtrl.DisableCtrl();
    }

    public override void OnStay()
    {

    }

    public override void OnExit()
    {
        this.bossCtrl.EnableCtrl();
    }

    public override void HandleInput()
    {

    }

}
