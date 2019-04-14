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

    public GameObject rangeIndicatorPf;
    public GameObject arrowIndicatorPf;
    public GameObject circleIndicatorPf;

    private GameObject rangeIndicatorGo;
    private GameObject arrowIndicatorGo;
    private GameObject circleIndicatorGo;

    private RangeIndicator rangeIndicator;  //范围指示器脚本
    private ArrowIndicator arrowIndicator; //箭头指示器脚本
    private CircleIndicator circleIndicator; //圆圈指示器脚本

    public GameObject DamageUI;

    public GameObject frozonEfx;  //冰冻特效

    public GameObject canvas;

    private void Awake()
    {
        instance = this;

    }

    public void ShowRangeIndicator(float range)
    {
        if (this.rangeIndicatorGo == null)
        {
            this.rangeIndicatorGo = Instantiate<GameObject>(this.rangeIndicatorPf);
            this.rangeIndicator = rangeIndicatorGo.GetComponent<RangeIndicator>();
        }
        rangeIndicator.Show(range);
    }

    public void HideRangeIndicator()
    {
        if (this.rangeIndicatorGo == null)
            return;
        rangeIndicator.Hide();
    }

    public void ShowCircleIndicator(Vector3 pos, float effectRange)
    {
        if (this.circleIndicatorGo == null)
        {
            this.circleIndicatorGo = Instantiate<GameObject>(this.circleIndicatorPf);
            this.circleIndicator = circleIndicatorGo.GetComponent<CircleIndicator>();
        }
        circleIndicator.Show(pos, effectRange);
    }

    public void HideCircleIndicator()
    {
        if (this.circleIndicatorGo == null)
            return;
        circleIndicator.Hide();
    }

    public void ShowArrowIndicator(float effectRange, float angle)
    {
        if (this.arrowIndicatorGo == null)
        {
            this.arrowIndicatorGo = Instantiate<GameObject>(this.arrowIndicatorPf);
            this.arrowIndicator = arrowIndicatorGo.GetComponent<ArrowIndicator>();
        }

        arrowIndicator.Show(effectRange, angle);
    }

    public void HideArrowIndcator()
    {
        if (arrowIndicatorGo == null)
            return;
        arrowIndicator.Hide();
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
