using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class RoleBaseCtrl : MonoBehaviour
{

    //todo animation fsm

    protected RoleData roleData;
    protected ActorBuff actorBuff;
    protected Animator anim;

    public int roleId;

    public RoleData RoleData
    {
        get { return this.roleData; }
    }


    protected CharacterController cc;

    public virtual void Awake()
    {
        cc = this.gameObject.GetComponent<CharacterController>();
        actorBuff = this.gameObject.AddComponent<ActorBuff>();
        roleData = new RoleData(RoleConfig.GetRoleDataById(this.roleId));
        roleData.SetCtrl(this);
        anim = GetComponent<Animator>();

    }

    public virtual void SimpleMove(Vector3 speed)
    {
        cc.SimpleMove(speed);
    }

    public virtual void RotateTo(Vector3 target)
    {

        cc.transform.rotation = Quaternion.Slerp(cc.transform.rotation, Quaternion.LookRotation(target), 0.2f);
    }

    public virtual void Die()
    {
        NpcManager.Instance.RemoveNpc(this.transform);

        bool death = anim.GetBool("death");

        this.enabled = false;

        Destroy(gameObject, 3);
        this.roleData.speed = 0;

        if (!death)
        {
            anim.SetBool("death", true);

        }
    }

    public void EnableCtrl()
    {

        this.enabled = true;
    }

    public void DisableCtrl()
    {
        this.enabled = false;
    }

    public void UpdateHp(int amount)
    {
        this.roleData.hp += amount;
        if (this.roleData.hp <= 0)
        {
            Die();
        }
    }

}
