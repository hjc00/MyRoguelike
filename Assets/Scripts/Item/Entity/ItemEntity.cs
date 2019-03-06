using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{

    public int cfgId = 0;

    private BaseItem item;

    private void Awake()
    {
        GetConfig();
    }

    public void GetConfig()
    {
        if (cfgId == 0)
        {
            Debug.Log("cfg id invalid");
            return;
        }

        item = ItemConfig.Instance.GetItemByCfgId(this.cfgId);
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerCtrl playerCtrl = other.transform.GetComponent<PlayerCtrl>();
        if (playerCtrl == null)
            return;

        item.Use(playerCtrl);
        Destroy(this.gameObject);
    }
}
