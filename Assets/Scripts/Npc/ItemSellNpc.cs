using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemSellNpc : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.AddListener(EventType.DestroyItemNpc, DestroySelf);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.DestroyItemNpc, DestroySelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameObject panel = UIManager.Instance.PopPanel(GameDefine.itemNpcPanel);

            panel.GetComponent<ItemNpcPanel>().SetData(this.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            UIManager.Instance.ClosePanel(GameDefine.itemNpcPanel);
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
