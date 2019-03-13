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

    public void Forward(Transform transform, Vector3 pos, float duration)
    {

        transform.GetComponent<RoleBaseCtrl>().DisableCtrl();
        Tween tween = transform.DOMove(pos, duration);
        tween.OnComplete(transform.GetComponent<RoleBaseCtrl>().EnableCtrl);
    }
}
