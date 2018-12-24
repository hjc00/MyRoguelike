using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetBool(string name, bool b)
    {
        anim.SetBool(name, b);
    }

    public void SetFloat(string name, float amout)
    {
        anim.SetFloat(name, amout);
    }

    public void SetTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    public void ResetTrigger(string name)
    {
        anim.ResetTrigger(name);
    }



    public void OnFsmEnter(string[] msgs)
    {

    }
}
