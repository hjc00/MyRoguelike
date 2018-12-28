using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region PlayerStates
public class PlayerIdle : FsmBase
{
    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerIdle(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnStay()
    {
        base.OnStay();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}


public class PlayerRun : FsmBase
{
    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerRun(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnStay()
    {
        base.OnStay();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

public class PlayerDie : FsmBase
{
    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayerDie(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnStay()
    {
        base.OnStay();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

public class PlayeAttack : FsmBase
{
    Animator anim;

    PlayerCtrl playerCtrl;

    public PlayeAttack(Animator animator, PlayerCtrl ctrl)
    {
        anim = animator;
        playerCtrl = ctrl;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnStay()
    {
        base.OnStay();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
#endregion

public enum PlayerAnimationEnum
{
    Idle,
    Run,
    Attak1,
    Attack2,
    Attack3,


    Max,
}

#region PlayerData

public class PlayerData
{
    int atkPower = 10;   //攻击力
    public int AtkPower
    {
        get { return atkPower; }
        set { atkPower = value; }
    }

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

    void Awake()
    {
        playData = new PlayerData();

        anim = GetComponent<Animator>();

        fsmManager = new FsmManager((int)PlayerAnimationEnum.Max);

    }

    public void DoRectDamage()
    {
        NpcManager.Instance.DoRectDamage(PlayerData.RectForward, PlayerData.RectWidth, PlayerData.AtkPower);
    }

    private void Update()
    {

    }

}
#endregion