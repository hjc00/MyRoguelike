using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : FsmBase
{
    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerIdle(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("idle state");
    }

    public override void OnStay()
    {
        HandleInput();
    }

    public override void OnExit()
    {

    }

    public override void HandleInput()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    anim.SetTrigger("attack");
        //    return;
        //}

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            float x = Input.GetAxis("Horizontal");

            float z = Input.GetAxis("Vertical");

            Vector3 target = new Vector3(x, 0, z);

            anim.SetFloat("velocity", target.magnitude);

        }
    }
}
