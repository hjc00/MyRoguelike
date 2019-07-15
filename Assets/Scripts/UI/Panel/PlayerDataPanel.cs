using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataPanel : MonoBehaviour
{

    private Text mpText;
    private Text atkText;
    private Text defText;

    PlayerCtrl ctrl;


    private void Awake()
    {
        // Debug.Log("player data panel init");
        mpText = transform.Find("mpText").GetComponent<Text>();
        atkText = transform.Find("atkText").GetComponent<Text>();
        defText = transform.Find("defText").GetComponent<Text>();

        ctrl = NpcManager.Instance.Player.GetComponent<PlayerCtrl>();

        //Debug.Log(this);
        EventCenter.AddListener<int>(EventType.OnUpdateMP, this.UpdateMpText);
        EventCenter.AddListener<int>(EventType.OnUpdateAtk, this.UpdateAtkText);
        EventCenter.AddListener<int>(EventType.OnUpdateDef, this.UpdateDefText);

    }

    void Start()
    {
        UpdateAtkText(ctrl.RoleData.atkPower);
        UpdateDefText(ctrl.RoleData.defPower);
        UpdateMpText(ctrl.RoleData.mp);
    }

    private void UpdateMpText(int amount)
    {
        if (this.mpText == null)
        {
            mpText = transform.Find("mpText").GetComponent<Text>();

        }
        this.mpText.text = "魔法值：" + amount;
    }

    private void UpdateAtkText(int amount)
    {
        //Debug.Log(this);
        if (this == null)
        {
            // GameObject.FindWithTag("Canvas").transform.Find("PlayerDataPanel").GetComponent<PlayerDataPanel>();
            return;
        }

        if (this.atkText == null)
        {
            //  Debug.Log(transform);
            atkText = transform.Find("atkText").GetComponent<Text>();

        }
        this.atkText.text = "攻击力：" + amount;
    }

    private void UpdateDefText(int amount)
    {

        if (this.defText == null)
        {
            atkText = transform.Find("atkText").GetComponent<Text>();
        }
        this.defText.text = "防御力：" + amount;
    }


    private void OnDestroy()
    {
        // Debug.Log("player data panel destroy");
        EventCenter.RemoveListener<int>(EventType.OnUpdateMP, UpdateAtkText);
        EventCenter.RemoveListener<int>(EventType.OnUpdateAtk, UpdateMpText);
        EventCenter.RemoveListener<int>(EventType.OnUpdateDef, UpdateDefText);
    }

}
