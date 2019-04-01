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

    public void Forward(Transform transform, Vector3 pos, float duration, float maxDistance)
    {

        transform.GetComponent<RoleBaseCtrl>().DisableCtrl();
        Tween tween;
        RaycastHit hit;

        transform.LookAt(pos);
        if (!Physics.Raycast(transform.position, pos - transform.position, out hit, maxDistance, LayerMask.NameToLayer("Layer")))
        {
            //Vector3 tmpDir = hit.transform.position - transform.position;
            //Debug.Log(tmpDir.magnitude);
            //tween = transform.DOMove(tmpDir, duration);

            tween = transform.DOMove(pos, duration);
            tween.OnComplete(transform.GetComponent<RoleBaseCtrl>().EnableCtrl);
        }
        else  //暂时射线碰到墙就反弹
        {
            Vector3 targetPos = transform.position - (maxDistance - (hit.transform.position - transform.position).magnitude) * (pos - transform.position).normalized;
            // Debug.Log(targetPos.magnitude);
            tween = transform.DOMove(targetPos, duration);
            transform.GetComponent<RoleBaseCtrl>().EnableCtrl();
        }
        // else
        // {

        //  }


    }
}
