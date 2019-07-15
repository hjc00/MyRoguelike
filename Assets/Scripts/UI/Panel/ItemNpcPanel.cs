using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemNpcPanel : BasePanel
{

    private int[] randomItemId = new int[3];

    public ItemCellUI[] itemCellUIs;
    public GameObject buyPanel;

    public int selectItemId { get; set; }
    Vector3 npcPos;

    public override void Awake()
    {
        RandomItemData();
        SetUpCell();
    }

    private void RandomItemData()
    {
       // Debug.Log("item data");

        randomItemId[0] = Random.Range(0, ItemConfig.Instance.GetItemCount()) + 1000;
        randomItemId[1] = Random.Range(0, ItemConfig.Instance.GetItemCount()) + 1000;
        randomItemId[2] = Random.Range(0, ItemConfig.Instance.GetItemCount()) + 1000;
    }

    public void SetData(Vector3 pos)
    {
        this.npcPos = pos;
    }

    void SetUpCell()
    {
        for (int i = 0; i < randomItemId.Length; i++)
        {
            itemCellUIs[i].SetData(randomItemId[i], this);
        }
    }

    public void ShowBuy()
    {
        buyPanel.SetActive(true);
        buyPanel.transform.DOScale(1, 0.2f);
    }

    public void CloseBuy()
    {

        buyPanel.transform.DOScale(0, 0.2f).OnComplete(() =>
        {
            buyPanel.SetActive(false);
        });
    }

    public void ClickBuy()
    {
        this.CloseBuy();

        UIManager.Instance.ClosePanel(GameDefine.itemNpcPanel);

        ItemConfig.Instance.CreateItemObj(this.npcPos + new Vector3(1, -1, 1), this.selectItemId);

        EventCenter.Broadcast(EventType.DestroyItemNpc);
    }
}
