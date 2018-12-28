using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : FsmBase
{

    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerRun(Animator animator, PlayerCtrl ctrl)
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
        Debug.Log("run state");
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

            Vector3 target = this.playerCtrl.transform.position + (new Vector3(x, 0, z) - this.playerCtrl.transform.position);

            //if (x != 0 || z != 0)
           // {
                Quaternion rot = Quaternion.LookRotation(target, playerCtrl.transform.up);

                playerCtrl.transform.rotation = Quaternion.Slerp(this.playerCtrl.transform.rotation, rot, 0.5f);

           // }

            playerCtrl.SimpleMove(target * playerCtrl.PlayerData.Speed);

            anim.SetFloat("velocity", target.magnitude);

            if (x == 0 && z == 0)
            {
                playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
            }
        }
    }
}
