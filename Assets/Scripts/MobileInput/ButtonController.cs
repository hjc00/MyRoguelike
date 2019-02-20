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
        btnA.onClick.AddListener(OnBtnAClick);
        btnB.onClick.AddListener(OnBtnBClick);
        btnC.onClick.AddListener(OnBtnCClick);
        btnAtk.onClick.AddListener(OnBtnAtkClick);

        playerCtrl = NpcManager.Instance.Player.gameObject.GetComponent<PlayerCtrl>();
    }

    private void OnBtnAClick()
    {


    }
    private void OnBtnBClick()
    {



    }
    private void OnBtnCClick()
    {


    }
    private void OnBtnAtkClick()
    {
        playerCtrl.FsmManager.ChangeState((int)PlayerAnimationEnum.Attack1);
        playerCtrl.DoRectDamage();
    }

}
