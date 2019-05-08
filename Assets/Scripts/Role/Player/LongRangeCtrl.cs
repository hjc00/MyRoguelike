using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LongRangeEnum
{
    Idle,
    Run,
    Attack,



    Max,
}

public class LongRangeCtrl : PlayerCtrl
{

    public GameObject castPos;


    private bool canAttack = true;



    public override void Awake()
    {
        base.Awake();
    }

    public override void FsmInit()
    {
        fsmManager = new FsmManager((int)LongRangeEnum.Max);

        PlayerIdle playerIdle = new PlayerIdle(anim, this);

        PlayerRun playerRun = new PlayerRun(anim, this);

        LongRangeAttack longRangeAttack = new LongRangeAttack(anim, this);


        fsmManager.AddState(playerIdle);

        fsmManager.AddState(playerRun);

        fsmManager.AddState(longRangeAttack);

        fsmManager.ChangeState((int)LongRangeEnum.Idle);

    }

    Vector3 dectectHalfExtents = new Vector3(10, 1, 10);

    float minDisSqr;

    Vector3 minDir;

    public override void Attack()
    {
        if (canAttack)
        {
            base.Attack();

            //Collider[] cols = Physics.OverlapBox(transform.position, dectectHalfExtents, Quaternion.identity, LayerMask.GetMask("Enemy"));

            this.canAttack = false;
            GameObject bullet = ObjectPool.Instance.SpawnObj("bullet", castPos.transform.position, transform.rotation);

            //if (cols.Length > 0)
            //{
            //    for (int i = 0; i < cols.Length; i++)
            //    {

            //        float tempSqr = Vector3.SqrMagnitude(cols[0].transform.position - this.transform.position);
            //        if (tempSqr < minDisSqr)
            //        {
            //            minDisSqr = tempSqr;
            //            minDir = cols[0].transform.position - this.transform.position;
            //        }
            //    }

            //    this.transform.LookAt(transform.position + minDir);

            //    // bullet.transform.LookAt(castPos.transform.position + new Vector3((cols[0].transform.position - castPos.transform.position).x, castPos.transform.position.y, (cols[0].transform.position - castPos.transform.position).z));
            //}
            bullet.GetComponent<CastCtrl>().Fly(Vector3.forward);

        }

    }

    private void DetectEnemy()
    {

    }

    public override void Die()
    {
        base.Die();
    }

    public override void ChangeToIdle()
    {
        fsmManager.ChangeState((int)LongRangeEnum.Idle);
    }

    public override void ChangeToRun()
    {
        fsmManager.ChangeState((int)LongRangeEnum.Run);
    }

    public void ChangeToAttack()
    {
        fsmManager.ChangeState((int)LongRangeEnum.Attack);
    }

    public void SetAttack()
    {
        this.canAttack = true;
    }
}
