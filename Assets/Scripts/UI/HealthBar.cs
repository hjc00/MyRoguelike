using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{




    private void Start()
    {
        NpcManager.Instance.Player.GetComponent<PlayerCtrl>().onPlayerHealthReduce += Test;
    }

    private void Test()
    {
        Debug.Log("test");
    }
}
