using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmBase
{

    public virtual void OnEnter()
    {

    }


    public virtual void OnStay()
    {
        HandleInput();
    }

    public virtual void OnExit()
    {

    }


    public virtual void HandleInput()
    {
 
    }
}
