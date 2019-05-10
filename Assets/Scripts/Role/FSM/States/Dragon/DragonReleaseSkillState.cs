using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonReleaseSkillState : FsmBase
{

    Animator anim;
    BossCtrl dragonCtrl;

    public DragonReleaseSkillState(Animator anim, BossCtrl ctrl)
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
