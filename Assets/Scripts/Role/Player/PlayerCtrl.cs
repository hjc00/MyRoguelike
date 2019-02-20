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

#region PlayerData

public class PlayerData : RoleData
{

    int rectForward = 10;   //矩形攻击范围长度
    public int RectForward
    {
        get { return rectForward; }
        set { rectForward = value; }
    }


    int rectWidth = 5;  //矩形攻击范围宽度
    public int RectWidth
    {
        get { return rectWidth; }
        set { rectWidth = value; }
    }

    int sectorAngle = 30;   //扇形攻击范围角度
    public int SectorAngle
    {
        get { return sectorAngle; }
        set { sectorAngle = value; }
    }

    int sectorRadius = 30;   //扇形攻击范围半径
    public int SectorRadius
    {
        get { return sectorRadius; }
        set { sectorRadius = value; }
    }

    int circleRadius = 10;   //原形攻击范围半径
    public int CircleRadius
    {
        get { return circleRadius; }
        set { circleRadius = value; }
    }

}
#endregion



#region PlayerFsmCtrl Mono

public class PlayerCtrl : RoleBaseCtrl
{
    private PlayerData playData;

    public PlayerData PlayerData
    {
        get { return playData; }
    }

    Animator anim;

    FsmManager fsmManager;

    public FsmManager FsmManager
    {
        get { return fsmManager; }
    }

    public override void Awake()
    {
        base.Awake();

        playData = new PlayerData();

        anim = GetComponent<Animator>();

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

    private void Update()
    {
        fsmManager.FsmUpdate();
    }

    private void ChangeToAttack1()
    {

    }

    private void ChangeToAttack2()
    {

    }

    private void ChangeToAttack3()
    {
    }

    public void DoRectDamage()
    {
        NpcManager.Instance.DoRectDamage(PlayerData.RectForward, PlayerData.RectWidth, PlayerData.AtkPower);
    }

    public void DoSectorDamage()
    {
        NpcManager.Instance.DoSectorDamage(PlayerData.SectorAngle, PlayerData.SectorRadius, PlayerData.AtkPower);
    }

    public void DoCircleDamage()
    {
        NpcManager.Instance.DoCircleDamage(PlayerData.CircleRadius, PlayerData.AtkPower);
    }


}
#endregion