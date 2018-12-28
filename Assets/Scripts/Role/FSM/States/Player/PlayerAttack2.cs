using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : FsmBase {

	Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerAttack2(Animator animator, PlayerCtrl ctrl)
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
