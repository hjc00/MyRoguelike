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
        Debug.Log("persue a player");
    }

    public override void OnStay()
    {
        anim.SetBool("run", true);

        if (Vector3.Distance(enemyCtrl.transform.position, NpcManager.Instance.Player.position) < enemyCtrl.EnemyData.AttackRange)
        {
            anim.SetBool("run", false);
            anim.SetTrigger("attack");
            return;
        }
        enemyCtrl.RotateTo(NpcManager.Instance.Player.position - enemyCtrl.transform.position);

        enemyCtrl.SimpleMove(NpcManager.Instance.Player.position - enemyCtrl.transform.position);

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
