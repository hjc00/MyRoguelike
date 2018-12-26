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


#region PlayerCtrl Mono
public class PlayerCtrl : MonoBehaviour
{

    Animator anim;
    FsmManager fsmManager;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();

        fsmManager = new FsmManager((int)PlayerAnimationEnum.Max);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
#endregion