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

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        enemyData = new EnemyData();

        fsmManager = new FsmManager((int)GoblinAnimationEnum.Max);

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

    public override void SimpleMove(Vector3 speed)
    {
        //  base.SimpleMove(enemyData.Speed);
    }

    private void Update()
    {
        fsmManager.FsmUpdate();
    }

    private void ChangeToIdle()
    {
        fsmManager.ChangeState((int)GoblinAnimationEnum.Idle);
    }


    public void ReduceHealth(int amount)
    {
       // fsmManager.ChangeState((int)GoblinAnimationEnum.Hit);

        enemyData.Health -= amount;
        Debug.Log(enemyData.Health);
        if (enemyData.Health <= 0)
        {
             fsmManager.ChangeState((int)GoblinAnimationEnum.Die);
            Debug.Log(enemyData.Health);
            return;
        }

        // Debug.Log(enemyData.Health);
    }
}
