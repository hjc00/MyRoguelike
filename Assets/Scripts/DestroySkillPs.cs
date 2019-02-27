using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySkillPs : MonoBehaviour
{

    public float destroyTime = 5;

    void Start()
    {
        Destroy(this.gameObject, destroyTime);

    }


}
