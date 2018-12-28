using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : FsmBase {

    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerRun(Animator animator, PlayerCtrl ctrl)
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
        anim.SetInteger("Index", (int)PlayerAnimationEnum.Run);
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
