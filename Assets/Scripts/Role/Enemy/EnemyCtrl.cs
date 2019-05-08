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


public class EnemyCtrl : RoleBaseCtrl
{


    protected FsmManager FsmManager { get; set; }


    private Sensor sensor;

    protected float sensorTimer = 0;
    protected float sensorCheckInterval = 0.1f;


    public override void Awake()
    {
        base.Awake();
        NpcManager.Instance.AddNpc(this.transform);

    }

    private void Start()
    {


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
        base.SimpleMove(dir.normalized * roleData.speed);
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

    public virtual void ChangeToDie()
    {
        FsmManager.ChangeState((int)GoblinAnimationEnum.Die);
    }

    public virtual void ChangeToAttack()
    {
        FsmManager.ChangeState((int)GoblinAnimationEnum.Attack);
    }

    public override void Die()
    {

        EventCenter.Broadcast<int>(EventType.OnAddGold, Random.Range(1, 5));
        base.Die();
    }

    public virtual void ReduceHealth(int amount)
    {
        if (roleData.hp <= 0)
            return;

        anim.SetTrigger("hit");

        roleData.hp -= amount;

        EffectPerform.Instance.ShowDamageUI(amount, this.transform);


        if (roleData.hp <= 0)
        {
            Die();
        }
    }


    public virtual void DoPlayerDamage()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            return;
        }
        NpcManager.Instance.DoPlayerDamage(this.transform, roleData.rectLength, roleData.rectWidth, roleData.atkPower);

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(this.transform.position + this.transform.forward * EnemyData.AtkForward, new Vector3(EnemyData.AtkWidth, 2, EnemyData.AtkWidth));
    //    Gizmos.color = Color.red;
    //}
}
