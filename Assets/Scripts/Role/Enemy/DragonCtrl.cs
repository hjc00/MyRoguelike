using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DragonAnimEnum
{
    Idle,
    Die,
    Hit,
    Attack,
    Persue,
    RealseSkill,
    Max,
}

public class DragonCtrl : EnemyCtrl
{

    private Sensor sensor;

    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {

        EnemyData = new EnemyData(5, 3, 250, 6, 20, 20);
        sensor = new Sensor(10, 120, this);

        FsmManager = new FsmManager((int)DragonAnimEnum.Max);


        DragonIdleState dragonIdleState = new DragonIdleState(anim, this);
        DragonDieState dragonDieState = new DragonDieState(anim, this);
        DragonHitState dragonHitState = new DragonHitState(anim, this);
        DragonAttackState dragonAttackState = new DragonAttackState(anim, this);
        DragonPersueState dragonPersueState = new DragonPersueState(anim, this);
        DragonReleaseSkillState dragonReleaseSkillState = new DragonReleaseSkillState(anim, this);

        FsmManager.AddState(dragonIdleState);
        FsmManager.AddState(dragonDieState);
        FsmManager.AddState(dragonHitState);
        FsmManager.AddState(dragonAttackState);
        FsmManager.AddState(dragonPersueState);
        FsmManager.AddState(dragonReleaseSkillState);


        FsmManager.ChangeState((int)DragonAnimEnum.Idle);

    }

    void Update()
    {
        FsmManager.FsmUpdate();
        sensorTimer += Time.deltaTime;


        if (sensorTimer > sensorCheckInterval)
        {
            sensor.SensorUpdate();
        }
    }

    public override void ChangeToPersue()
    {
        FsmManager.ChangeState((int)DragonAnimEnum.Persue);
    }

    public override void ChangeToHit()
    {
        FsmManager.ChangeState((int)DragonAnimEnum.Hit);
    }

    public override void ReduceHealth(int amount)
    {
        if (EnemyData.Health <= 0)
            return;

        anim.SetTrigger("hit");

        EnemyData.Health -= amount;

        EffectPerform.Instance.ShowDamageUI(amount, this.transform);


        if (EnemyData.Health <= 0)
        {
            bool death = anim.GetBool("death");

            if (!death)
            {
                this.enabled = false;
                anim.SetBool("death", true);
            }
        }
    }

    public override void DoPlayerDamage()
    {
        base.DoPlayerDamage();
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(this.transform.position + this.transform.forward * EnemyData.AtkForward, new Vector3(EnemyData.AtkWidth, 2, EnemyData.AtkWidth));
    //    Gizmos.color = Color.red;


    //}
}
