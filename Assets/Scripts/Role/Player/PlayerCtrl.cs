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

    private AudioCtrl audioCtrl;

    private GameObject RangeIndicator;
    private GameObject ArrowIndicator;
    private GameObject CircleIndicator;

    public GameObject frozonPs;

    public delegate void OnPlayerHealthReduce();
    public event OnPlayerHealthReduce onPlayerHealthReduce;

    public override void Awake()
    {
        base.Awake();

        playData = new PlayerData();

        anim = GetComponent<Animator>();

        audioCtrl = GetComponent<AudioCtrl>();

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

    public void ShowCircleIndicator(int radius, Vector3 dir, float quotient)
    {


        CircleIndicator.SetActive(true);
        float multi = radius * 0.2f * 0.936f;   //施法范围圆圈的缩放比例

        Debug.Log(RangeIndicator.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f);
        Debug.Log(RangeIndicator.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f);
        Debug.Log(RangeIndicator.GetComponent<SpriteRenderer>().size);


        CircleIndicator.transform.localScale = new Vector3(0.1f * multi, 0.1f * multi, 1);
        // Debug.Log(dir);

        float percent = RangeIndicator.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f * quotient;
        Debug.Log("quotient " + quotient);
        Debug.Log("percent " + percent);

        Vector3 converDir = new Vector3(dir.x, 0.01f, dir.y);   //dir是屏幕坐标系，x y轴
                                                                // Debug.Log(converDir);
        CircleIndicator.transform.position = this.transform.position + converDir.normalized * percent;
        Debug.Log(CircleIndicator.transform.position);
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

    public void ReduceHealth(int amount)
    {
        if (playData.Health <= 0)
            return;

        anim.SetTrigger("hit");

        playData.Speed = 0;

        playData.Health -= amount;

        onPlayerHealthReduce();

        //Debug.Log(enemyData.Health);

        ShowDamageUI(amount);

        if (playData.Health <= 0)
        {
            bool death = anim.GetBool("death");

            if (!death)
            {
                this.enabled = false;
                anim.SetBool("death", true);
            }
        }
    }

    public void ShowDamageUI(int amount)
    {
        transform.GetComponent<RoleUI>().ShowDamage(amount);
    }
    #endregion


    #region 技能相关
    public void ReleaseFrozonSkill(int radius)
    {
        GameObject frozonGo = Instantiate(this.frozonPs);
        frozonGo.transform.position = CircleIndicator.transform.position;
        Debug.Log(frozonGo.transform.position + "------------" + CircleIndicator.transform.position);

        NpcManager.Instance.CircleCheck(this.CircleIndicator.transform.position, radius);
    }
    #endregion

    #region 声音相关
    public void PlayAttackSound()
    {
        audioCtrl.PlayAttackSound();
        Debug.Log("playAttackSound");
    }
    #endregion
}
#endregion