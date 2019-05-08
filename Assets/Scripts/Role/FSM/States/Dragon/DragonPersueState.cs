using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPersueState : FsmBase {

    Animator anim;
    DragonCtrl dragonCtrl;

    public DragonPersueState(Animator anim, DragonCtrl ctrl)
    {
        this.anim = anim;
        this.dragonCtrl = ctrl;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnStay()
    {
        anim.SetBool("run", true);

        if (Vector3.Distance(dragonCtrl.transform.position, NpcManager.Instance.Player.position) < dragonCtrl.RoleData.rectLength)
        {
            anim.SetBool("run", false);

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                anim.SetTrigger("attack");
            }
            return;
        }
        dragonCtrl.RotateTo(NpcManager.Instance.Player.position - dragonCtrl.transform.position);

        dragonCtrl.SimpleMove(NpcManager.Instance.Player.position - dragonCtrl.transform.position);
    }

    public override void OnExit()
    {
      
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
}
