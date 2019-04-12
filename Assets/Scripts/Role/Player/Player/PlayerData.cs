using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerData : RoleData
{

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

    public PlayerData(int rectForward, int rectWidth, int sectorAngle, int sectorRadius, int circleRadius)
    {
        this.rectForward = rectForward;
        this.rectWidth = rectWidth;
        this.sectorAngle = sectorAngle;
        this.sectorRadius = sectorRadius;
        this.circleRadius = circleRadius;
    }

    public PlayerData(int rectForward, int rectWidth, int sectorAngle, int sectorRadius, int circleRadius,
        int health, int speed, int atkPower, int defPower) : base(health, speed, atkPower, defPower)
    {
        this.rectForward = rectForward;
        this.rectWidth = rectWidth;
        this.sectorAngle = sectorAngle;
        this.sectorRadius = sectorRadius;
        this.circleRadius = circleRadius;
    }
}