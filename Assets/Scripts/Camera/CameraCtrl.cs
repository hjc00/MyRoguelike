using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraCtrl : MonoBehaviour
{
    private static CameraCtrl instacne;

    public static CameraCtrl Instance
    {
        get
        {
            return instacne;
        }
    }

    private Vector3 offset;
    private Transform target;

    private void Awake()
    {
        instacne = this;

    }

    void Start()
    {
        target = NpcManager.Instance.Player;

        offset = this.transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = target.position + offset;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 0.1f);
    }

    public void CameraShake(float duration,float strength)
    {
        
        Tween tween = this.transform.DOShakePosition(duration,strength);
    }

    public void CameraShake(float duration, Vector3 strength)
    {
        Tween tween = this.transform.DOShakePosition(duration, strength);
    }
}
