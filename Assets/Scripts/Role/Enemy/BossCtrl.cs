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

public class BossCtrl : EnemyCtrl
{


    private Sensor sensor;

    public override void Awake()
    {
        base.Awake();
    }

    BossSkill bossSkill;

    public override void Start()
    {
        bossSkill = GetComponent<BossSkill>();

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


    private float skillTimer = 0;
    private float skillCooldown = 10;
    private bool canReleaseSkill = true;

    public override void Update()
    {
        if (this.roleData.hp <= 0)
            return;

        skillTimer += Time.deltaTime;
        if (skillTimer >= skillCooldown)
        {
            canReleaseSkill = true;
        }

        FsmManager.FsmUpdate();
        sensorTimer += Time.deltaTime;
        if (sensorTimer > sensorCheckInterval)
        {
            sensor.SensorUpdate();
        }
    }

    public void ReleaseSkill()
    {
        if (this.canReleaseSkill == false)
            return;

        anim.SetTrigger("releaseSkill");

        bossSkill.Use();
        this.canReleaseSkill = false;
        this.skillTimer = 0;
    }

    public override void ChangeToPersue()
    {
        FsmManager.ChangeState((int)DragonAnimEnum.Persue);
    }

    public override void ChangeToHit()
    {
        FsmManager.ChangeState((int)DragonAnimEnum.Hit);
    }

    public override void ChangeToIdle()
    {
        FsmManager.ChangeState((int)DragonAnimEnum.Idle);
    }

    public override void ChangeToAttack()
    {
        FsmManager.ChangeState((int)DragonAnimEnum.Attack);
    }

    public override void Die()
    {

        EventCenter.Broadcast(EventType.OnBossDie);
    }

    public override void ReduceHealth(int amount)
    {
        if (RoleData.hp <= 0)
            return;

        anim.SetTrigger("hit");
        // Debug.Log(this.RoleData.hp);
        RoleData.hp -= amount;

        EffectPerform.Instance.ShowDamageUI(amount, this.transform);


        if (RoleData.hp <= 0)
        {
            bool death = anim.GetBool("death");
            // Debug.Log("broadcast boss die");
            Die();

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
