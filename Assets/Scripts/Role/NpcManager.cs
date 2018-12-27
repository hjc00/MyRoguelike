using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{

    private static NpcManager instance;

    public static NpcManager Instance
    {
        get { return instance; }
    }

    private List<Transform> npcs;   //保存对所有npc的引用

    private Transform player;

    private void Awake()
    {
        instance = this;

        npcs = new List<Transform>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private bool CheckInRect(Transform attack, Transform attacked, int forward, int width)
    {
        Vector3 dir = attacked.position - attack.position;

        float dot = Vector3.Dot(dir, attack.forward);  //得到方向在攻击者前方的投影

        if (dot > 0 && dot < forward)    //dot > 0 表示在前方，dot < forward 表示在范围内s
        {
            if (Mathf.Abs(Vector3.Dot(dir, attack.right.normalized)) < width)
            {
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// //矩形攻击伤害
    /// </summary>
    /// <param name="forward">  前向长度    </param>
    /// <param name="width">    宽度  </param>
    /// <param name="power">    攻击力 </param>
    public void DoRectDamage(int forward, int width, int power)
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            if (CheckInRect(player, npcs[i], forward, width))
            {
                npcs[i].GetComponent<RoleNpcCtrl>().ReduceHealth(power);
            }
        }
    }


    private bool CheckInSector(Transform attack, Transform attacked, int angle, int radius)
    {
        Vector3 dir = attacked.position - attack.position;


        if (Mathf.Acos(Vector3.Dot(dir.normalized, attack.forward)) * Mathf.Rad2Deg < angle * 0.5f && dir.magnitude < radius)
        {
            return true;
        }

        return false;
    }

    public void DoSectorDamage(int angle, int radius, int power)   //扇形攻击伤害
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            if (CheckInSector(player, npcs[i], angle, radius))
            {
                npcs[i].GetComponent<RoleNpcCtrl>().ReduceHealth(power);
            }
        }
    }

    private bool CheckInCircle(Transform attack, Transform attacked, int radius)
    {
        if (Vector3.Distance(attack.position, attacked.position) < radius)
        {
            return true;
        }

        return false;
    }

    public void DoCircleDamage(int radius, int power)
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            if (CheckInCircle(player, npcs[i], radius))
            {
                npcs[i].GetComponent<RoleNpcCtrl>().ReduceHealth(power);
            }
        }
    }
}
