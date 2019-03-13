using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class RoleBaseCtrl : MonoBehaviour
{

    //todo animation fsm

    protected RoleData roleData;

    public RoleData RoleData
    {
        get { return this.roleData; }
    }

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


    public void EnableCtrl()
    {
        Debug.Log("enble");
        this.enabled = true;
    }

    public void DisableCtrl()
    {
        this.enabled = false;
    }

}
