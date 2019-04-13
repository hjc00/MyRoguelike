﻿using System.Collections;
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

    public Transform Player
    {
        get { return player; }
        set
        {
            player = value;
        }
    }

    private void Awake()
    {
        instance = this;

        npcs = new List<Transform>();

        // player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void AddNpc(Transform go)
    {
        if (!this.npcs.Contains(go))
            this.npcs.Add(go);
    }

    public void RemoveNpc(Transform go)
    {
        if (!this.npcs.Contains(go))
            return;
        this.npcs.Remove(go);
    }

    public Vector3 GetPlayerPosition()
    {
        return player.position;
    }

    /// <summary>
    /// //玩家矩形攻击伤害
    /// </summary>
    /// <param name="attack">  攻击者    </param>
    /// <param name="attacked">    被攻击者  </param>
    /// <param name="forward">    前向长度 </param> 
    /// <param name="width">    宽度 </param> 
    private bool CheckInRect(Transform attack, Transform attacked, int forward, int width)
    {
        Vector3 dir = attacked.position - attack.position;

        float dot = Vector3.Dot(dir, attack.forward);  //得到方向在攻击者前方的投影

        // Debug.Log(dot);

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
    /// //玩家矩形攻击伤害
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
                npcs[i].GetComponent<EnemyCtrl>().ReduceHealth(power);
                CameraCtrl.Instance.CameraShake(0.1f, 0.5f);
                SkillPerform.Instance.BeatBack(npcs[i], npcs[i].position - player.position, 0.1f, 0.5f);
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
                npcs[i].GetComponent<EnemyCtrl>().ReduceHealth(power);
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
                npcs[i].GetComponent<EnemyCtrl>().ReduceHealth(power);
            }
        }
    }

    public void ReduceSpeed(Vector3 center, int radius)          //以某一点为中心做圆形范围检测
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            float magSqr = (npcs[i].position - center).sqrMagnitude;
            if (magSqr <= radius * radius)
            {
                Debug.Log("reduce speed");
                npcs[i].GetComponent<EnemyCtrl>().ReduceSpeed(2, 5);   //fix 持续时间
            }
        }

    }

    public void DoPlayerDamage(Transform enemy, int forward, int width, int power)
    {

        if (CheckInRect(enemy, player, forward, width))
        {
            player.GetComponent<PlayerCtrl>().ReduceHealth(power);
            SkillPerform.Instance.BeatBack(player, player.position - enemy.position, 0.1f, 0.5f);
        }
    }

    public void ClearAllEnemy()
    {
        npcs.Clear();
    }
}
