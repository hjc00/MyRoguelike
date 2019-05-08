using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPersueState : FsmBase
{

    Animator anim;
    EnemyCtrl enemyCtrl;

    public GoblinPersueState(Animator anim, EnemyCtrl ctrl)
    {
        this.anim = anim;
        this.enemyCtrl = ctrl;
    }

    public override void OnEnter()
    {
        //  Debug.Log("persue a player");
    }

    public override void OnStay()
    {
        anim.SetBool("run", true);
       // Debug.Log("persue stay");
        //  Debug.Log(Vector3.Distance(enemyCtrl.transform.position, NpcManager.Instance.Player.position) + " rect length " + enemyCtrl.RoleData.rectLength);
        enemyCtrl.RotateTo(NpcManager.Instance.Player.position - enemyCtrl.transform.position);

        enemyCtrl.SimpleMove(NpcManager.Instance.Player.position - enemyCtrl.transform.position);

        if (Vector3.Distance(enemyCtrl.transform.position, NpcManager.Instance.Player.position) < enemyCtrl.RoleData.rectLength + 0.5d)
        {
            anim.SetBool("run", false);

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                anim.SetTrigger("attack");
                this.enemyCtrl.RoleData.speed = 0;
            }
            return;
        }

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
