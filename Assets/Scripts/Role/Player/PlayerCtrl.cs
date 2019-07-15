using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerAnimationEnum
{
    Idle,
    Run,
    Attack1,
    Attack2,
    Attack3,


    Max,
}




#region PlayerFsmCtrl Mono

public class PlayerCtrl : RoleBaseCtrl
{

    protected FsmManager fsmManager;

    public FsmManager FsmManager
    {
        get { return fsmManager; }
    }

    public Transform cameraPos;

    public delegate void OnPlayerHealthReduce(int amount);
    public event OnPlayerHealthReduce onPlayerHealthReduce;

    public PlayerInventory playerInventory;

    public override void Awake()
    {
        base.Awake();

        playerInventory = new PlayerInventory();

        if (PlayerPrefs.GetInt("AtkPower") != 0)
        {
            this.roleData.atkPower = PlayerPrefs.GetInt("AtkPower");
        }
        if (PlayerPrefs.GetInt("DefPower") != 0)
        {
            this.roleData.defPower = PlayerPrefs.GetInt("DefPower");
        }
        if (PlayerPrefs.GetInt("MP") != 0)
        {
            this.roleData.mp = PlayerPrefs.GetInt("MP");
        }

        RegisterEvent();
        FsmInit();
      

    }

    public virtual void FsmInit()
    {
        fsmManager = new FsmManager((int)PlayerAnimationEnum.Max);

        PlayerIdle playerIdle = new PlayerIdle(anim, this);

        PlayerRun playerRun = new PlayerRun(anim, this);

        PlayerAttack1 playerAttack1 = new PlayerAttack1(anim, this);

        PlayerAttack2 playerAttack2 = new PlayerAttack2(anim, this);

        PlayerAttack3 playerAttack3 = new PlayerAttack3(anim, this);


        fsmManager.AddState(playerIdle);

        fsmManager.AddState(playerRun);

        fsmManager.AddState(playerAttack1);

        fsmManager.AddState(playerAttack2);

        fsmManager.AddState(playerAttack3);

        fsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
    }

    void RegisterEvent()
    {
        EventCenter.AddListener<int>(EventType.OnAddGold, AddGold);
    }

    void UnregisterEvent()
    {
        EventCenter.RemoveListener<int>(EventType.OnAddGold, AddGold);
    }

    private void OnDestroy()
    {
        UnregisterEvent();
    }

    private void Update()
    {
        fsmManager.FsmUpdate();
    }

    private void AddGold(int amount)
    {
        playerInventory.Gold += amount;
    }


    #region 状态机相关接口
    public virtual void ChangeToIdle()
    {

        fsmManager.ChangeState((int)PlayerAnimationEnum.Idle);
    }

    public void ChangeToAttack1()
    {
        fsmManager.ChangeState((int)PlayerAnimationEnum.Attack1);
    }

    public void ChangeToAttack2()
    {

        fsmManager.ChangeState((int)PlayerAnimationEnum.Attack2);
    }

    public void ChangeToAttack3()
    {
        fsmManager.ChangeState((int)PlayerAnimationEnum.Attack3);
    }

    public virtual void ChangeToRun()
    {
        fsmManager.ChangeState((int)PlayerAnimationEnum.Run);
    }

    public virtual void Attack()
    {
        anim.SetTrigger("attack");

    }


    #endregion

    #region    攻击相关
    public void DoRectDamage()
    {
        NpcManager.Instance.DoRectDamage(this.transform.position + this.transform.forward * roleData.rectLength * 0.5f,
         roleData.rectLength, roleData.rectWidth, roleData.atkPower);
    }


    public void ReduceHealth(int amount)
    {
        if (roleData.hp <= 0)
            return;

        anim.SetTrigger("hit");

        roleData.hp -= amount;

        onPlayerHealthReduce(roleData.hp);


        EffectPerform.Instance.ShowDamageUI(amount, this.transform);

        if (roleData.hp <= 0)
        {
            //  Debug.Log("death trigger");
            anim.SetTrigger("death");

            this.roleData.speed = 0;

            this.cc.enabled = false;
            UIManager.Instance.PopPanel(GameDefine.restartPanel);
            this.enabled = false;
            //   Time.timeScale = 0;
        }
    }

    public void AddHealth(int amount)
    {
        roleData.hp += amount;
        onPlayerHealthReduce(roleData.hp);
    }

    public void UpdateAtkPower(int amount)
    {
        //  Debug.Log("amount " + amount);
        this.roleData.atkPower += amount;
        EventCenter.Broadcast<int>(EventType.OnUpdateAtk, this.roleData.atkPower);
        PlayerPrefs.SetInt("AtkPower", this.roleData.atkPower);

    }

    //public void UpdateSpeed(int amount)
    //{
    //    this.roleData.speed += amount;
    //    EventCenter.Broadcast<int>(EventType.OnUpdateAtk, this.roleData.atkPower);
    //}

    public void UpdateDefPower(int amount)
    {
        //Debug.Log("amount " + amount);
        this.roleData.defPower += amount;
        EventCenter.Broadcast<int>(EventType.OnUpdateDef, this.roleData.defPower);
        PlayerPrefs.SetInt("DefPower", this.roleData.defPower);
    }

    public void UpdateMp(int amount)
    {
        Debug.Log("amount " + amount);
        this.roleData.mp += amount;
        EventCenter.Broadcast<int>(EventType.OnUpdateMP, this.roleData.mp);
        PlayerPrefs.SetInt("MP", this.roleData.mp);
    }

    #endregion

    //private void OnDrawGizmos()
    //{
    //    //Gizmos.DrawSphere(NpcManager.Instance.Player.position + NpcManager.Instance.Player.forward * 3, 2);
    //    Gizmos.DrawCube(transform.position + transform.forward * roleData.rectLength * 0.5f, new Vector3(roleData.rectLength * 0.5f, 1, roleData.rectWidth * 0.5f));
    //    Gizmos.color = Color.red;
    //}
}
#endregion