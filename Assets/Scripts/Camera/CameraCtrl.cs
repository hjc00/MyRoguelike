using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    private Vector3 offset;
    private Transform target;
    void Start()
    {
        target = NpcManager.Instance.Player;

        offset = this.transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = target.position + offset;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 0.5f);
    }
}
