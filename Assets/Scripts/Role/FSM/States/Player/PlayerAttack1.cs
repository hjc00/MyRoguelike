using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1 : FsmBase {

    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerAttack1(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
    
    }


    float timer = 0;
    public override void OnStay()
    {

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
