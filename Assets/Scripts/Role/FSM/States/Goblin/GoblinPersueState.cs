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
        // Debug.Log("persue stay");
       // Debug.Log(Vector3.Distance(enemyCtrl.transform.position, NpcManager.Instance.Player.position) + " rect length " + enemyCtrl.RoleData.rectLength);
        if (Vector3.Distance(enemyCtrl.transform.position, NpcManager.Instance.Player.position) < enemyCtrl.RoleData.rectLength - 1)
        {
            anim.SetBool("run", false);

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                anim.SetTrigger("attack");

                this.enemyCtrl.RoleData.speed = 0;
            }
            return;
        }
        else
        {
            anim.SetBool("run", true);

            enemyCtrl.RotateTo(NpcManager.Instance.Player.position - enemyCtrl.transform.position);

            enemyCtrl.SimpleMove(NpcManager.Instance.Player.position - enemyCtrl.transform.position);

        }

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
