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

    public virtual void RotateTo(Vector3 target)
    {
        cc.transform.rotation = Quaternion.Slerp(cc.transform.rotation, Quaternion.LookRotation(target), 0.2f);
    }

}
