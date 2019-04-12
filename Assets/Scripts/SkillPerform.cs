using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillPerform
{
    private static SkillPerform instance;

    public static SkillPerform Instance
    {
        get
        {
            if (instance == null)
            {
                return new SkillPerform();
            }
            return instance;
        }
    }

    //带位置重载版本
    public void Forward(Transform transform, Vector3 pos, float duration, float maxDistance)
    {
        if (transform == null)
            return;

        transform.GetComponent<RoleBaseCtrl>().DisableCtrl();
        Tween tween;
        RaycastHit hit;

        transform.LookAt(pos);
        if (!Physics.Raycast(transform.position, pos - transform.position, out hit, maxDistance))
        {
            tween = transform.DOMove(pos, duration);
            tween.OnComplete(transform.GetComponent<RoleBaseCtrl>().EnableCtrl);
        }
        else  //暂时射线碰到墙就反弹
        {

            float differ = maxDistance - (hit.transform.position - transform.position).magnitude;
            if (differ <= 0)
            {
                differ = 0;
            }
            Vector3 targetPos = transform.position - differ * (pos - transform.position).normalized;
            tween = transform.DOMove(targetPos, duration);
            transform.GetComponent<RoleBaseCtrl>().EnableCtrl();
        }
        // else
        // {

        //  }


    }

    //默认前冲重载版本
    public void Forward(Transform transform, float duration, float maxDistance,float checkForward)
    {
        if (transform == null)
            return;

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, checkForward))
        {
          //  Debug.Log(123123123);
            return;
        }


        transform.GetComponent<RoleBaseCtrl>().DisableCtrl();
        Tween tween;
        Vector3 pos = transform.position + (transform.forward * maxDistance);

        transform.LookAt(pos);

        tween = transform.DOMove(pos, duration);
        tween.OnComplete(transform.GetComponent<RoleBaseCtrl>().EnableCtrl);


    }

    public void BeatBack(Transform transform, float duration, float backDistance)  //默认向后击退
    {
        if (transform == null)
            return;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.forward, out hit, 1))
        {
            return;
        }

        transform.GetComponent<RoleBaseCtrl>().DisableCtrl();
        Tween tween;


        tween = transform.DOMove(transform.position + (-transform.forward * backDistance), duration);
        tween.OnComplete(transform.GetComponent<RoleBaseCtrl>().EnableCtrl);
    }

    public void BeatBack(Transform transform, Vector3 dir, float duration, float backDistance)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, 1))
        {
            return;
        }

        transform.GetComponent<RoleBaseCtrl>().DisableCtrl();
        Tween tween;
        Vector3 pos = transform.position + dir * backDistance;
        tween = transform.DOMove(pos, duration);
        tween.OnComplete(transform.GetComponent<RoleBaseCtrl>().EnableCtrl);
    }


}
