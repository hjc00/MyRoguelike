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
        Debug.Log("run state");
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


        float x = 0;
        float z = 0;



        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                x = JoystickController.Instance.GetX();

                z = JoystickController.Instance.GetY();
            }
            else
            {

                x = Input.GetAxis("Horizontal");

                z = Input.GetAxis("Vertical");
            }

            Vector3 target = new Vector3(x, 0, z);

            if (x != 0 || z != 0)

                playerCtrl.transform.rotation = Quaternion.Slerp(playerCtrl.transform.rotation, Quaternion.LookRotation(target), 0.2f);


            playerCtrl.SimpleMove(target * playerCtrl.RoleData.speed);


            anim.SetFloat("velocity", target.magnitude);

        }
    }
}
