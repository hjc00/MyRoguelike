using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPersueState : FsmBase
{

    Animator anim;
    BossCtrl ctrl;

    public DragonPersueState(Animator anim, BossCtrl ctrl)
    {
        this.anim = anim;
        this.ctrl = ctrl;
    }

    public override void OnEnter()
    {

    }

    public override void OnStay()
    {

        ctrl.RotateTo(NpcManager.Instance.Player.position - ctrl.transform.position);

        if (Vector3.Distance(ctrl.transform.position, NpcManager.Instance.Player.position) < ctrl.RoleData.rectLength)
        {

            if (Random.Range(0, 100) < 50)
            {
                ctrl.ReleaseSkill();
                return;
            }

            anim.SetBool("run", false);

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                anim.SetTrigger("attack");
            }
            return;
        }
        else
        {
            anim.SetBool("run", true);


            ctrl.SimpleMove(NpcManager.Instance.Player.position - ctrl.transform.position);
        }
    }

    public override void OnExit()
    {

    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
}
