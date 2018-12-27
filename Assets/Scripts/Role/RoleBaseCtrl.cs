using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RoleBaseCtrl : MonoBehaviour
{

    //todo animation fsm

    private CharacterController cc;

    void Awake()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    public virtual void SimpleMove(Vector3 speed)
    {
        cc.SimpleMove(speed);
    }

}
