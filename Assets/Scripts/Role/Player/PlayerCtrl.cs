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

    private GameObject RangeIndicator;
    private GameObject ArrowIndicator;
    private GameObject CircleIndicator;

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

        RangeIndicator = transform.Find("RangeIndicator").gameObject;
        RangeIndicator.SetActive(false);

        ArrowIndicator = transform.Find("ArrowIndicator").gameObject;
        ArrowIndicator.SetActive(false);

        CircleIndicator = transform.Find("CircleIndicator").gameObject;
        CircleIndicator.SetActive(false);
    }

    private void Update()
    {
        fsmManager.FsmUpdate();
    }

    #region 状态机相关接口
    public void ChangeToIdle()
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

    public void ChangeToRun()
    {
        fsmManager.ChangeState((int)PlayerAnimationEnum.Run);
    }
    #endregion


    #region    技能UI相关
    public void ShowRangeIndicator(int radius)
    {
        RangeIndicator.SetActive(true);
        float multi = radius * 0.2f * 0.936f;
        //Debug.Log(RangeIndicator.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f);
        RangeIndicator.transform.localScale = new Vector3(0.1f * multi, 0.1f * multi, 1);
    }

    public void HideRangeIndicator()
    {
        RangeIndicator.SetActive(false);
    }

    public void ShowArrowIndicator(int radius, float angle)
    {
        if (ArrowIndicator.activeSelf == false)
            ArrowIndicator.SetActive(true);
        // Debug.Log(ArrowIndicator.GetComponentInChildren<SpriteRenderer>().bounds.size.x);  //6.96
        float multi = radius / 6.5f;
        ArrowIndicator.transform.localScale = new Vector3(multi, 1, 1);

        ArrowIndicator.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    public void HideArrowIndicator()
    {
        ArrowIndicator.SetActive(false);
    }

    public void ShowCircleIndicator(int radius, Vector3 pos)
    {

        ////先计算相机到目标的向量
        //Vector3 dir = pos - Camera.main.transform.position;
        ////计算投影
        //Vector3 normardir = Vector3.Project(dir, Camera.main.transform.forward);
        ////计算是节点，需要知道处置屏幕的投影距离
        //Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, normardir.magnitude));

        //worldpos.y = 0.01f;


        CircleIndicator.SetActive(true);
        float multi = radius * 0.2f * 0.936f;
        //Debug.Log(RangeIndicator.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f);
        CircleIndicator.transform.localScale = new Vector3(0.1f * multi, 0.1f * multi, 1);
        // CircleIndicator.transform.position =  worldpos;
        CircleIndicator.transform.position = pos;
    }

    public void HideCircleIndicator()
    {
        CircleIndicator.SetActive(false);
    }
    #endregion

    #region    攻击相关
    public void DoRectDamage()
    {
        NpcManager.Instance.DoRectDamage(PlayerData.RectForward, PlayerData.RectWidth, PlayerData.AtkPower);
    }

    public void DoSectorDamage()
    {
        anim.SetTrigger("attack");
        NpcManager.Instance.DoSectorDamage(PlayerData.SectorAngle, PlayerData.SectorRadius, PlayerData.AtkPower);
    }

    public void DoCircleDamage()
    {
        anim.SetTrigger("attack");
        NpcManager.Instance.DoCircleDamage(PlayerData.CircleRadius, PlayerData.AtkPower);
    }
    #endregion

}
#endregion