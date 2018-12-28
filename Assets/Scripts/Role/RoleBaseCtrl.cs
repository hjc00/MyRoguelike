using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RoleBaseCtrl : MonoBehaviour
{

    //todo animation fsm

    protected CharacterController cc;

    public virtual void Awake()
    {
        cc = this.gameObject.GetComponent<CharacterController>();

    }

    public virtual void SimpleMove(Vector3 speed)
    {
        cc.SimpleMove(speed);
    }

}
