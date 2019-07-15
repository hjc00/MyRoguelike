using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{

    public GameObject bossCast;

    public void Use()
    {
        for (int i = 0; i < 360; i += 30)
        {

            Vector3 v = Quaternion.Euler(0, i, 0) * this.transform.forward;

            Vector3 pos = this.transform.position + v;

            GameObject tempVfx = Instantiate(bossCast, this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.FromToRotation(this.transform.forward, v));

            tempVfx.GetComponent<BossCastCtrl>().Fly(Vector3.forward);
        }
    }
}
