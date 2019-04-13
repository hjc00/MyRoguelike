using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillSellNpc : MonoBehaviour {

    private void Awake()
    {
        EventCenter.AddListener(EventType.DestroySkillNpc, DestroySelf);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.DestroySkillNpc, DestroySelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            UIManager.Instance.PopPanel(GameDefine.skillNpcPanel);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            UIManager.Instance.ClosePanel(GameDefine.skillNpcPanel);
        }
    }

    private void DestroySelf()
    {
        transform.DOScale(0, 1f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
