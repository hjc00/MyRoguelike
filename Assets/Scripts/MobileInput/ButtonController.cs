using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button btnA;
    public Button btnB;
    public Button btnC;
    public Button btnAtk;

    PlayerCtrl playerCtrl;

    private void Start()
    {

        btnAtk.onClick.AddListener(OnBtnAtkClick);

        playerCtrl = NpcManager.Instance.Player.gameObject.GetComponent<PlayerCtrl>();
    }


    private void OnBtnAtkClick()
    {
        playerCtrl.transform.GetComponent<Animator>().SetTrigger("attack");
        // playerCtrl.DoRectDamage();
    }

}
