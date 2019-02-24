using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;

public enum SkillBtnType
{

    Direction,   //方向
    Sector,   //扇形
    Circle,    //圆形
    Choose
}

public class SkillBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SkillBtnType type;
    public GameObject RangeSprite;  //在角色地下生成的施法范围圆圈
    private Transform btnRangeSprite;  //按钮按下时的范围圆圈
    private Transform btnCtrlSprite;   //按钮按下时标记位置的sprite
    float radius = 80;
    private Vector2 originPos;

    private void Start()
    {
        btnRangeSprite = transform.Find("Range");
        btnCtrlSprite = transform.Find("point");
        originPos = btnCtrlSprite.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        btnCtrlSprite.position = eventData.position;

        if (Vector2.Distance(btnCtrlSprite.position, originPos) > radius)
        {

            this.btnCtrlSprite.position = originPos + (eventData.position - originPos).normalized * radius;
        }
        else
        {
            this.btnCtrlSprite.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        btnRangeSprite.gameObject.SetActive(true);
        btnCtrlSprite.gameObject.SetActive(true);

        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        btnRangeSprite.gameObject.SetActive(false);
        btnCtrlSprite.gameObject.SetActive(false);
    }
}
