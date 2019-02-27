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
    private int attackRange = 3;
    public int AttackRange
    {
        get { return attackRange; }
    }
}

public class EnemyCtrl : RoleBaseCtrl
{
    Animator anim;

    private EnemyData enemyData;

    public EnemyData EnemyData
    {
        get { return enemyData; }
    }

    private FsmManager fsmManager;

    public FsmManager FsmManager
    {
        get { return fsmManager; }
    }

    private Sensor sensor;

    private float sensorTimer = 0;
    private float sensorCheckInterval = 0.1f;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        enemyData = new EnemyData();
        enemyData.Speed = 3;

        fsmManager = new FsmManager((int)GoblinAnimationEnum.Max);
        sensor = new Sensor(10, 120, this);

        NpcManager.Instance.AddNpc(this.transform);

        GoblinAttackState goblinAttackState = new GoblinAttackState(anim, this);
        GoblinDieState goblinDieState = new GoblinDieState(anim, this);
        GoblinHitState goblinHitState = new GoblinHitState(anim, this);
        GoblinIdleState goblinIdleState = new GoblinIdleState(anim, this);
        GoblinPatrolState goblinPatrolState = new GoblinPatrolState(anim, this);
        GoblinPersueState goblinPersueState = new GoblinPersueState(anim, this);

        fsmManager.AddState(goblinIdleState);
        fsmManager.AddState(goblinDieState);
        fsmManager.AddState(goblinHitState);
        fsmManager.AddState(goblinAttackState);
        fsmManager.AddState(goblinPatrolState);
        fsmManager.AddState(goblinPersueState);

        fsmManager.ChangeState((int)GoblinAnimationEnum.Idle);
    }

    public override void SimpleMove(Vector3 dir)
    {
        base.SimpleMove(dir.normalized * enemyData.Speed);
    }

    private void Update()
    {
        sensorTimer += Time.deltaTime;
        fsmManager.FsmUpdate();
        if (sensorTimer > sensorCheckInterval)
        {
            sensor.SensorUpdate();
        }
    }

    private void ChangeToIdle()
    {
        fsmManager.ChangeState((int)GoblinAnimationEnum.Idle);
    }


    private void ChangeToHit()
    {
        FsmManager.ChangeState((int)GoblinAnimationEnum.Hit);
    }

    public void ChangeToPersue()
    {
        FsmManager.ChangeState((int)GoblinAnimationEnum.Persue);
    }

    public void ReduceHealth(int amount)
    {

        if (enemyData.Health <= 0)
            return;

        anim.SetTrigger("hit");

        enemyData.Health -= amount;

        Debug.Log(enemyData.Health);

        if (enemyData.Health <= 0)
        {
            bool death = anim.GetBool("death");
            Debug.Log(death);
            if (!death)
            {
                this.enabled = false;
                anim.SetBool("death", true);
            }
        }
    }

    public void ReduceSpeed(int amount,int time)  //减少敌人速度
    {
        if (amount > enemyData.Speed)
        {
            enemyData.Speed = 0;
            StartCoroutine(ResumeSpeed(5));
            return;
        }
        enemyData.Speed -= amount;
        StartCoroutine(ResumeSpeed(5));
    }

    IEnumerator ResumeSpeed(int time)
    {
        yield return time;
        enemyData.Speed -= 3;
    }
}
