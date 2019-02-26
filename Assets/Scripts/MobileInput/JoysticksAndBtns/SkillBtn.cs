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
    public int skillRange = 5;
    private Transform btnRangeSprite;  //按钮按下时的范围圆圈
    private Transform btnCtrlSprite;   //按钮按下时标记位置的sprite
    float radius = 80;
    private Vector2 originPos;
    private PlayerCtrl playerCtrl;

    private void Start()
    {
        btnRangeSprite = transform.Find("Range");
        btnCtrlSprite = transform.Find("Point");
        originPos = btnCtrlSprite.position;
        // Debug.Log(originPos);

        playerCtrl = NpcManager.Instance.Player.GetComponent<PlayerCtrl>();
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

        Vector2 dir = new Vector2(eventData.position.x - originPos.x, eventData.position.y - originPos.y);
        //Debug.Log(dir);

        float angle = Vector3.Angle(dir, originPos);

        if (dir.x > 0 && dir.y > 0)
            angle = -angle;

        if (dir.x < 0 && dir.y > 0)
            angle = -angle;


        switch (type)
        {
            case SkillBtnType.Direction:
                playerCtrl.ShowArrowIndicator(skillRange, angle);
                break;
            case SkillBtnType.Sector:

                break;
            case SkillBtnType.Circle:
                {

                    playerCtrl.ShowCircleIndicator(2, this.btnCtrlSprite.position);
                }
                break;
            case SkillBtnType.Choose:
                break;
            default:
                break;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        btnRangeSprite.gameObject.SetActive(true);
        btnCtrlSprite.gameObject.SetActive(true);

        playerCtrl.ShowRangeIndicator(skillRange);

        Vector2 dir = new Vector2(eventData.position.x - originPos.x, eventData.position.y - originPos.y);



        float angle = Vector3.Angle(dir, originPos);
        if (dir.x > 0 && dir.y > 0)
            angle = -angle;

        switch (type)   //fix  拓展性
        {
            case SkillBtnType.Direction:
                playerCtrl.ShowArrowIndicator(skillRange, angle);
                break;
            case SkillBtnType.Sector:

                break;
            case SkillBtnType.Circle:
                {
                    Vector3 pos = eventData.position;
                    Debug.Log(pos);
                    playerCtrl.ShowCircleIndicator(2, eventData.position);
                }
                break;
            case SkillBtnType.Choose:
                break;
            default:
                break;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        btnRangeSprite.gameObject.SetActive(false);
        btnCtrlSprite.gameObject.SetActive(false);


        Vector2 dir = new Vector2(eventData.position.x - originPos.x, eventData.position.y - originPos.y);


        switch (type)   //fix  拓展性
        {
            case SkillBtnType.Direction:
                {
                    playerCtrl.HideArrowIndicator();
                    ReleaseForwardSkill(new Vector3(dir.x, 0, dir.y));
                }
                break;
            case SkillBtnType.Sector:

                break;
            case SkillBtnType.Circle:
                {
                    playerCtrl.HideCircleIndicator();
                }
                break;
            case SkillBtnType.Choose:
                break;
            default:
                break;
        }

        playerCtrl.HideRangeIndicator();

    }

    private void ReleaseForwardSkill(Vector3 dir)    //fix  拓展性
    {
        playerCtrl.transform.LookAt(dir);
        playerCtrl.transform.position += dir.normalized * 8;
    }


}
