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
        Debug.Log("idle state");
        base.OnEnter();
    }

    public override void OnStay()
    {
        HandleInput();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Attack1);
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            float x = Input.GetAxis("Horizontal");

            float z = Input.GetAxis("Vertical");

            Vector3 target = new Vector3(x, 0, z);

            // if (x < 0.5 || z < 0.5)

            //    playerCtrl.transform.rotation = Quaternion.Slerp(playerCtrl.transform.rotation, Quaternion.LookRotation(playerCtrl.transform.position + target), 0.9f);

            if (x != 0 || z != 0)
            {
                playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Run);
            }

        }
    }
}
