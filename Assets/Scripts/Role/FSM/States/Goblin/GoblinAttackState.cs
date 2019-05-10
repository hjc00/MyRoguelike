using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackState : FsmBase
{

    Animator anim;
    EnemyCtrl enemyCtrl;

    private int originSpeed = 5;

    public GoblinAttackState(Animator anim, EnemyCtrl ctrl)
    {
        this.anim = anim;
        this.enemyCtrl = ctrl;
        originSpeed = this.enemyCtrl.RoleData.speed;
    }

    public override void OnEnter()
    {
        //  Debug.Log("goblin atk enter");
        this.enemyCtrl.RoleData.speed = 0;
        //  Debug.Log(this.enemyCtrl.RoleData.speed);
    }

    public override void OnStay()
    {
       // enemyCtrl.SimpleMove((NpcManager.Instance.Player.position - enemyCtrl.transform.position) * 0);
        // Debug.Log("atk stay");
    }

    public override void OnExit()
    {
        this.enemyCtrl.RoleData.speed = originSpeed;
        //   Debug.Log(this.enemyCtrl.RoleData.speed);
    }

    public override void HandleInput()
    {

    }
}
