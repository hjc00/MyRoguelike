using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor
{

    public int viewRadius { get; private set; }
    public int viewAngle { get; private set; }
    private EnemyCtrl ctrl;

    public Sensor(int viewRadius, int viewAngle, EnemyCtrl ctrl)
    {
        this.viewRadius = viewRadius;
        this.viewAngle = viewAngle;
        this.ctrl = ctrl;
    }

    public void SensorUpdate()
    {
        if (CheckIsInView())
        {
            ctrl.ChangeToPersue();
            ctrl.SetFollowTarget(NpcManager.Instance.Player);
        }
    }

    private bool CheckIsInView()  //判断是否在视野范围内
    {
        if (ctrl == null || NpcManager.Instance.Player == null)
            return false;
        Vector3 dir = NpcManager.Instance.Player.position - ctrl.transform.position;

        // Debug.Log(dir.magnitude + "     " + this.viewRadius);

        if (Mathf.Acos(Vector3.Dot(dir.normalized, ctrl.transform.forward)) * Mathf.Rad2Deg < this.viewAngle * 0.5f
            && dir.magnitude < this.viewRadius)
        {
            return true;
        }

        return false;
    }

}
