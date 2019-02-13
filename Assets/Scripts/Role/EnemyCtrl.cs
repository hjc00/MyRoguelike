using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyData : RoleData
{

}

public class EnemyCtrl : RoleBaseCtrl
{


    private EnemyData enemyData;


    public override void Awake()
    {
        base.Awake();
        enemyData = new EnemyData();
        NpcManager.Instance.AddNpc(this.transform);
    }

    public override void SimpleMove(Vector3 speed)
    {
        //  base.SimpleMove(enemyData.Speed);
    }


    public void ReduceHealth(int amount)
    {

        enemyData.Health -= amount;
        if (enemyData.Health <= 0)
        {

        }
        Debug.Log(enemyData.Health);
    }
}
