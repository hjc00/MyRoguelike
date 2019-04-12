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
    private float magnitude;

    private void Awake()
    {
        instacne = this;

        target = NpcManager.Instance.Player.GetComponent<PlayerCtrl>().cameraPos;

        offset = target.position - NpcManager.Instance.Player.position;

        Follow();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPos = NpcManager.Instance.Player.position + offset;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 0.1f);
    }

    public void CameraShake(float duration, float strength)
    {

        Tween tween = this.transform.DOShakePosition(duration, strength);
    }

    public void CameraShake(float duration, Vector3 strength)
    {
        Tween tween = this.transform.DOShakePosition(duration, strength);
    }
}
