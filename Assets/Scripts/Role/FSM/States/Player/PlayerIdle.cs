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
        playerCtrl.PlayerData.Speed = 8;
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

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            float x = 0;
            float z = 0;

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

            anim.SetFloat("velocity", target.magnitude);

        }
    }



}
