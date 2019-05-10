using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBuff : MonoBehaviour
{
    public List<Buff> buffs = new List<Buff>();

    public void AddBuff(RoleData roleData, Buff buff)
    {
        if (buffs.Contains(buff))
            return;
        buffs.Add(buff);
       // Debug.Log("start buff " + buff.id);
        buff.StartBuff(roleData, this);
    }

    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
        {
          //  Debug.Log("remove " + buff.id);
            buffs.Remove(buff);
        }
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].buffSettleType.Equals(BuffSettleType.Loop))
                buffs[i].Tick(Time.fixedDeltaTime);
        }
    }

}
