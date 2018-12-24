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

    private void Start()
    {
        btnA.onClick.AddListener(OnBtnAClick);
        btnB.onClick.AddListener(OnBtnBClick);
        btnC.onClick.AddListener(OnBtnCClick);
        btnAtk.onClick.AddListener(OnBtnAtkClick);

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
        PlayerController.Instance.SetTrigger("attack");
    }

}
