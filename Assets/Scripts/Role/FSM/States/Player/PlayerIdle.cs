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
    }

    public override void OnStay()
    {
        anim.SetInteger("Index", (int)PlayerAnimationEnum.Idle);

        if (Input.GetMouseButtonDown(0))
        {
            playerCtrl.FsmManager.ChangeState((sbyte)PlayerAnimationEnum.Attack1);
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            float x = Input.GetAxis("Horizontal");

            float y = Input.GetAxis("Vertical");

            Vector3 target = this.playerCtrl.transform.position + (new Vector3(x, 0, y) - this.playerCtrl.transform.position);

            if (x != 0 || y != 0)
            {
                Quaternion rot = Quaternion.LookRotation(target, this.playerCtrl.transform.up);

                if (rot != null)

                    this.playerCtrl.transform.rotation = Quaternion.Slerp(this.playerCtrl.transform.rotation, rot, 0.6f);

            }

            if (target.magnitude > 0.1)
            {
                playerCtrl.FsmManager.ChangeState((sbyte)PlayerAnimationEnum.Run);

                playerCtrl.SimpleMove(target * playerCtrl.PlayerData.Speed);
            }
            else
                playerCtrl.FsmManager.ChangeState((sbyte)PlayerAnimationEnum.Idle);
        }

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
