using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EffectPerform : MonoBehaviour
{

    private static EffectPerform instance;
    public static EffectPerform Instance
    {
        get { return instance; }
    }

    public GameObject DamageUI;

    public GameObject frozonEfx;  //冰冻特效

    public GameObject canvas;


    private void Awake()
    {
        instance = this;
        //canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void ShowDamageUI(int amount, Transform appearTrans)
    {
        GameObject damageUI = Instantiate(this.DamageUI);

        damageUI.transform.position = Camera.main.WorldToScreenPoint(appearTrans.position + new Vector3(0, 2, 0));
        damageUI.GetComponent<Text>().text = "-" + amount.ToString();
        damageUI.transform.SetParent(canvas.transform);
    }

    public void PlayFrozenPs(Vector3 pos)
    {
        GameObject frozonGo = Instantiate(this.frozonEfx);
        frozonGo.transform.position = pos;
    }
}
