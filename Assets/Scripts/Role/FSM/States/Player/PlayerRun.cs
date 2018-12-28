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

            Vector3 target = new Vector3(x, 0, z);

            //playerCtrl.transform.LookAt(this.playerCtrl.transform.position + target);

            playerCtrl.transform.rotation = Quaternion.Slerp(playerCtrl.transform.rotation, Quaternion.LookRotation(playerCtrl.transform.forward + target), 0.9f);

            playerCtrl.SimpleMove(target * playerCtrl.PlayerData.Speed);

            Debug.Log(target);

            anim.SetFloat("velocity", target.magnitude);

            if (x == 0 && z == 0)
            {
                playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
            }
        }
    }
}
