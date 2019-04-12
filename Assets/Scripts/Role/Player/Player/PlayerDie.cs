using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : FsmBase
{

    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerDie(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnStay()
    {
        base.OnStay();
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
