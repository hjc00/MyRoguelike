using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public enum BuffType
{
    AddHp = 1,
    AddMp = 2,
    AddAtkPower = 3,
    AddDefPower = 4,
    AddSpeed = 5,
    SubHp = 6,
    SubMp = 7,
    SubAtkPower = 8,
    SubDefPower = 9,
    SubSpeed = 10,
}

public enum BuffSettleType
{
    Loop = 1,
    Permernant = 2,
}


[Serializable]
public class Buff
{
    public int id;
    public BuffType buffType;
    public BuffSettleType buffSettleType;
    public int duration;  //持续时间
    public int freq;    //响应间隔
    public int amount; //数值

    [JsonIgnore]
    public float timer = 0;
    [JsonIgnore]
    private RoleData roleData;
    [JsonIgnore]
    private ActorBuff actorBuff;


    public void StartBuff(RoleData roleData, ActorBuff actorBuff)
    {
        this.roleData = roleData;
        this.actorBuff = actorBuff;

        if (buffType.Equals(BuffType.AddHp))
        {
            roleData.hp += amount;
            EffectPerform.Instance.ShowDamageUI(amount, this.actorBuff.transform);

        }
        else if (buffType.Equals(BuffType.AddSpeed))
        {
            roleData.speed += amount;

        }
        else if (buffType.Equals(BuffType.AddAtkPower))
        {
            roleData.atkPower += amount;
        }
        else if (buffType.Equals(BuffType.AddDefPower))
        {
            roleData.defPower += amount;
        }

        if (buffType.Equals(BuffType.SubHp))
        {
            roleData.hp -= amount;
            EffectPerform.Instance.ShowDamageUI(amount, this.actorBuff.transform);

            if (roleData.hp <= 0)
            {
                roleData.ctrl.Die();
            }
        }
        else if (buffType.Equals(BuffType.SubSpeed))
        {

            roleData.speed -= amount;
            if (roleData.speed <= 0)
                roleData.speed = 0;

        }
        else if (buffType.Equals(BuffType.SubAtkPower))
        {
            roleData.atkPower -= amount;
        }
        else if (buffType.Equals(BuffType.SubDefPower))
        {
            roleData.defPower -= amount;

        }

        if (this.buffSettleType.Equals(BuffSettleType.Permernant))
        {

            actorBuff.RemoveBuff(this);
        }
    }

    public void Tick(float dt)
    {
        timer += dt;
        if (timer >= duration)
        {

            if (this.buffSettleType.Equals(BuffSettleType.Loop))
            {
                FinishBuff();
                timer = 0;
            }
        }
    }

    public void FinishBuff()
    {
        if (buffType.Equals(BuffType.AddHp))
        {
            roleData.hp -= amount;
        }
        else if (buffType.Equals(BuffType.AddSpeed))
        {
            roleData.speed -= amount;

        }
        else if (buffType.Equals(BuffType.AddAtkPower))
        {
            roleData.atkPower -= amount;
        }
        else if (buffType.Equals(BuffType.AddDefPower))
        {
            roleData.defPower -= amount;
        }

        if (buffType.Equals(BuffType.SubHp))
        {
            roleData.hp += amount;
        }
        else if (buffType.Equals(BuffType.SubSpeed))
        {
            roleData.speed += amount;
        }
        else if (buffType.Equals(BuffType.SubAtkPower))
        {
            roleData.atkPower += amount;
        }
        else if (buffType.Equals(BuffType.SubDefPower))
        {
            roleData.defPower += amount;
        }
        actorBuff.RemoveBuff(this);
    }
}

