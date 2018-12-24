using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<PlayerController>();
                    DontDestroyOnLoad(obj);
                }

            }
            return instance;
        }
    }

    private CharacterController cc;
    private AnimationController animationController;

    private Animator animator;

    public int speed = 5;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animationController = GetComponent<AnimationController>();
        animator = GetComponent<Animator>();
        instance = this;
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //只有在windows系统的webplayer平台上才会执行
            MobileMove();
        }
        else
        {
            KeyBoardMove();
        }
    }


    private void KeyBoardMove()
    {

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            float x = Input.GetAxis("Horizontal");

            float y = Input.GetAxis("Vertical");


            Vector3 target = this.transform.position + (new Vector3(x, 0, y) - this.transform.position);

            if (x != 0 || y != 0)
            {
                Quaternion rot = Quaternion.LookRotation(target, transform.up);

                transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, 0.5f);

            }

            Debug.Log(target.magnitude);

            SetFloat("velocity", target.magnitude);


            cc.SimpleMove(target * speed);

        }

    }

    private void MobileMove()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            float x = JoystickController.Instance.GetX();

            float y = JoystickController.Instance.GetY();

            Vector3 target = this.transform.position + (new Vector3(x, 0, y) - this.transform.position);

            if (x != 0 || y != 0)
            {
                Quaternion rot = Quaternion.LookRotation(target, transform.up);

                transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, 0.5f);
            }

            SetFloat("velocity", target.magnitude);

            cc.SimpleMove(target * speed);
        }
    }

    public void SetBool(string name, bool b)
    {
        animationController.SetBool(name, b);
    }

    public void SetFloat(string name, float amount)
    {
        animationController.SetFloat(name, amount);
    }

    public void SetTrigger(string name)
    {
        animationController.SetTrigger(name);
    }
}
