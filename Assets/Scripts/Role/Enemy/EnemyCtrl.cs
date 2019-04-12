using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GoblinAnimationEnum
{
    Idle,
    Die,
    Hit,
    Attack,
    Patrol,
    Persue,


    Max,
}

public class EnemyData : RoleData
{
    private int atkForward = 3;
    public int AtkForward
    {
        get { return atkForward; }
    }

    public int AtkWidth { get; private set; }

    public EnemyData(int atkForward, int atkWidth, int health, int speed, int atkPower, int defPower) : base(health, speed, atkPower, defPower)
    {
        this.atkForward = atkForward;
        this.AtkWidth = atkWidth;
    }
}

public class EnemyCtrl : RoleBaseCtrl
{
    protected Animator anim;


    public EnemyData EnemyData { get; set; }


    protected FsmManager FsmManager { get; set; }


    private Sensor sensor;

    protected float sensorTimer = 0;
    protected float sensorCheckInterval = 0.1f;


    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        NpcManager.Instance.AddNpc(this.transform);

    }

    private void Start()
    {

        EnemyData = new EnemyData(3, 2, 100, 3, 10, 10);

        sensor = new Sensor(10, 120, this);

        FsmManager = new FsmManager((int)GoblinAnimationEnum.Max);


        GoblinAttackState goblinAttackState = new GoblinAttackState(anim, this);
        GoblinDieState goblinDieState = new GoblinDieState(anim, this);
        GoblinHitState goblinHitState = new GoblinHitState(anim, this);
        GoblinIdleState goblinIdleState = new GoblinIdleState(anim, this);
        GoblinPatrolState goblinPatrolState = new GoblinPatrolState(anim, this);
        GoblinPersueState goblinPersueState = new GoblinPersueState(anim, this);

        FsmManager.AddState(goblinIdleState);
        FsmManager.AddState(goblinDieState);
        FsmManager.AddState(goblinHitState);
        FsmManager.AddState(goblinAttackState);
        FsmManager.AddState(goblinPatrolState);
        FsmManager.AddState(goblinPersueState);

        FsmManager.ChangeState((int)GoblinAnimationEnum.Idle);
    }

    public override void SimpleMove(Vector3 dir)
    {
        base.SimpleMove(dir.normalized * EnemyData.Speed);
    }

    private void Update()
    {
        FsmManager.FsmUpdate();
        sensorTimer += Time.deltaTime;
        if (sensorTimer > sensorCheckInterval)
        {
            sensor.SensorUpdate();
        }
    }

    public virtual void ChangeToIdle()
    {
        FsmManager.ChangeState((int)GoblinAnimationEnum.Idle);
    }


    public virtual void ChangeToHit()
    {
        FsmManager.ChangeState((int)GoblinAnimationEnum.Hit);
    }

    public virtual void ChangeToPersue()
    {
        FsmManager.ChangeState((int)GoblinAnimationEnum.Persue);
    }

    public virtual void ReduceHealth(int amount)
    {

        if (EnemyData.Health <= 0)
            return;

        anim.SetTrigger("hit");

        EnemyData.Health -= amount;

        EffectPerform.Instance.ShowDamageUI(amount, this.transform);


        if (EnemyData.Health <= 0)
        {
            bool death = anim.GetBool("death");
            this.enabled = false;
            NpcManager.Instance.RemoveNpc(this.transform);
            cc.enabled = false;
            EventCenter.Broadcast<int>(EventType.OnAddGold, Random.Range(1, 5));
            Destroy(gameObject, 3);
            if (!death)
            {
                anim.SetBool("death", true);

            }
        }
    }

    public void ReduceSpeed(int amount, int time)  //减少敌人速度
    {
        if (amount > EnemyData.Speed)
        {
            EnemyData.Speed = 0;
            StartCoroutine(ResumeSpeed(5));
            return;
        }
        EnemyData.Speed -= amount;
        StartCoroutine(ResumeSpeed(5));
    }

    IEnumerator ResumeSpeed(int time)
    {
        yield return time;
        EnemyData.Speed = 3;
    }

    public virtual void DoPlayerDamage()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            return;
        }
        NpcManager.Instance.DoPlayerDamage(this.transform, EnemyData.AtkForward, EnemyData.AtkWidth, EnemyData.AtkPower);
        //    Debug.Log(EnemyData.AtkForward);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(this.transform.position + this.transform.forward * EnemyData.AtkForward, new Vector3(EnemyData.AtkWidth, 2, EnemyData.AtkWidth));
    //    Gizmos.color = Color.red;
    //}
}
