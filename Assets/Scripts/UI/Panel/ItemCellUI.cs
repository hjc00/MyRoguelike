using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellUI : MonoBehaviour
{

    public Image icon;
    public Text desc;
    public Text gold;
    public Text itemName;

    private ItemNpcPanel itemNpcPanel;
    private int id;
    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetData(int id, ItemNpcPanel itemNpcPanel)
    {
        //  Debug.Log("item id " + id);
        this.itemNpcPanel = itemNpcPanel;
        this.id = id;
        BaseItem item = ItemConfig.Instance.GetItemByCfgId(id);
        icon.sprite = Resources.Load(GameDefine.itemIconPath + item.icon, typeof(Sprite)) as Sprite;
        desc.text = item.Desc;
        gold.text = item.gold.ToString();
        itemName.text = item.Name;

    }

    private void OnClick()
    {
        itemNpcPanel.selectItemId = this.id;
        itemNpcPanel.ShowBuy();
    }

}
