using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{

    public int angle = 60;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.up, angle * Time.deltaTime);
    }
}
