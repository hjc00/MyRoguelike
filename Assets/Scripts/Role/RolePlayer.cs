using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData
{
    int atkPower = 10;   //攻击力
    public int AtkPower
    {
        get { return atkPower; }
        set { atkPower = value; }
    }

    int rectForward = 10;   //矩形攻击范围长度
    public int RectForward
    {
        get { return rectForward; }
        set { rectForward = value; }
    }


    int rectWidth = 5;  //矩形攻击范围宽度
    public int RectWidth
    {
        get { return rectWidth; }
        set { rectWidth = value; }
    }

    int sectorAngle = 30;   //扇形攻击范围角度
    public int SectorAngle
    {
        get { return sectorAngle; }
        set { sectorAngle = value; }
    }

    int sectorRadius = 30;   //扇形攻击范围半径
    public int SectorRadius
    {
        get { return sectorRadius; }
        set { sectorRadius = value; }
    }

    int circleRadius = 10;   //原形攻击范围半径
    public int CircleRadius
    {
        get { return circleRadius; }
        set { circleRadius = value; }
    }

}
public class RolePlayer : RoleBase
{


    private PlayerData playData;

    public PlayerData PlayerData
    {
        get { return playData; }
    }

    private void Awake()
    {
        playData = new PlayerData();
    }


    public void DoRectDamage()
    {
        NpcManager.Instance.DoRectDamage(PlayerData.RectForward, PlayerData.RectWidth, PlayerData.AtkPower);
    }

    

}
