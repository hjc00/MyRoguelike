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
    private PlayerData playData;

    public PlayerData PlayerData
    {
        get { return playData; }
    }

    Animator anim;
    private ItemUseCtrl itemUse;
    FsmManager fsmManager;

    public FsmManager FsmManager
    {
        get { return fsmManager; }
    }

    private GameObject RangeIndicator;
    private GameObject ArrowIndicator;
    private GameObject CircleIndicator;

    public Transform cameraPos;

    public delegate void OnPlayerHealthReduce(int amount);
    public event OnPlayerHealthReduce onPlayerHealthReduce;

    private PlayerInventory playerInventory;

    public override void Awake()
    {
        base.Awake();

        playData = new PlayerData(4, 2, 60, 5, 5);
        anim = GetComponent<Animator>();
        playerInventory = new PlayerInventory();
        EventCenter.Broadcast<int>(EventType.OnUpdateGold, 0); ;

        RegisterEvent();
        FsmInit();

        RangeIndicator = transform.Find("RangeIndicator").gameObject;
        RangeIndicator.SetActive(false);

        ArrowIndicator = transform.Find("ArrowIndicator").gameObject;
        ArrowIndicator.SetActive(false);

        CircleIndicator = transform.Find("CircleIndicator").gameObject;
        CircleIndicator.SetActive(false);
    }

    void FsmInit()
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

        Debug.Log(radius);

        Debug.Log(RangeIndicator.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f);



        CircleIndicator.transform.localScale = new Vector3(0.1f * multi, 0.1f * multi, 1);
        // Debug.Log(dir);

        float percent = RangeIndicator.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f * quotient;

        Debug.Log(RangeIndicator.GetComponent<SpriteRenderer>().bounds.size.x);
        //  Debug.Log("quotient " + quotient);
        //  Debug.Log("percent " + percent);

        Vector3 converDir = new Vector3(dir.x, 0.01f, dir.y);   //dir是屏幕坐标系，x y轴
                                                                // Debug.Log(converDir);
        CircleIndicator.transform.position = this.transform.position + converDir.normalized * percent;
        // Debug.Log(CircleIndicator.transform.position);
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

        onPlayerHealthReduce(playData.Health);

        //Debug.Log(enemyData.Health);

        EffectPerform.Instance.ShowDamageUI(amount, this.transform);

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

    public void AddHealth(int amount)
    {
        playData.Health += amount;
        onPlayerHealthReduce(playData.Health);
    }

    #endregion


    #region 技能相关
    public void ReleaseFrozonSkill(int radius)
    {
        EffectPerform.Instance.PlayFrozenPs(this.CircleIndicator.transform.position);
        NpcManager.Instance.ReduceSpeed(this.CircleIndicator.transform.position, radius);
    }
    #endregion
    //private void OnDrawGizmos()
    //{
    //    //Gizmos.DrawSphere(NpcManager.Instance.Player.position + NpcManager.Instance.Player.forward * 3, 2);
    //    Gizmos.DrawCube(transform.position + transform.forward * playData.RectForward * 0.5f, new Vector3(playData.RectForward * 0.5f, 1, playData.RectWidth * 0.5f));
    //    Gizmos.color = Color.red;
    //}
}
#endregion