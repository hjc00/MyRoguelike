using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;

public enum SkillType
{
    AOE = 1,
    DIR = 2,
    SELECT = 3,
    INSTANT = 4
}

public class SkillBtnCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Transform btnRangeSprite;  //按钮按下时的范围圆圈
    private Transform btnCtrlSprite;   //按钮按下时标记位置的sprite
    float radius = 80;
    private Vector2 originPos;
    private PlayerCtrl playerCtrl;

    public int SkillId = -1;
    private int range;
    private int type;
    private int effectRange;

    private void Start()
    {
        btnRangeSprite = transform.Find("Range");
        btnCtrlSprite = transform.Find("Point");
        originPos = btnCtrlSprite.position;


        playerCtrl = NpcManager.Instance.Player.GetComponent<PlayerCtrl>();

    }

    public void SetSkillData(int skillId)
    {
        Skill tempSkill = SkillConfig.Instance.GetSkillById(skillId);
        this.SkillId = skillId;
        this.range = tempSkill.range;
        this.type = tempSkill.type;
        this.effectRange = tempSkill.effectRange;

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

        switch (this.type)
        {
            case (int)SkillType.DIR:
                {

                    EffectPerform.Instance.ShowArrowIndicator(this.range * 0.5f, angle);
                }; break;
            case (int)SkillType.SELECT:
                {


                    EffectPerform.Instance.ShowCircleIndicator(CalCircleIndicatorPos(dir), effectRange);
                }; break;

            default:
                break;
        }

    }

    private Vector3 CalCircleIndicatorPos(Vector2 dir)  //计算出圆圈指示器在的坐标（相对坐标）
    {
        float mag = (new Vector2(this.btnCtrlSprite.position.x, this.btnCtrlSprite.position.y) - this.originPos).magnitude;
        float quotient = mag / 80;//求出所占倍数;

        float percent = 7.12f * 0.5f * quotient;  //7.12为示范范围指示器的默认宽度

        Vector3 converDir = new Vector3(dir.x, 0.1f, dir.y);   //dir是屏幕坐标系，x y轴

        return converDir.normalized * percent;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.SkillId == -1)
        {
            UIManager.Instance.PopHint("该技能槽还没学习技能!");
            return;

        }

        btnRangeSprite.gameObject.SetActive(true);
        btnCtrlSprite.gameObject.SetActive(true);
        btnCtrlSprite.position = eventData.position;

        Vector2 dir = new Vector2(eventData.position.x - originPos.x, eventData.position.y - originPos.y);

        float angle = Vector3.Angle(dir, originPos);
        if (dir.x > 0 && dir.y > 0)
            angle = -angle;

        EffectPerform.Instance.ShowRangeIndicator(range);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.SkillId == -1)
        {
            return;
        }

        btnRangeSprite.gameObject.SetActive(false);
        btnCtrlSprite.gameObject.SetActive(false);


        Vector2 dir = new Vector2(eventData.position.x - originPos.x, eventData.position.y - originPos.y);

        EffectPerform.Instance.HideRangeIndicator();

        switch (this.type)
        {
            case (int)SkillType.DIR:
                {
                    EffectPerform.Instance.HideArrowIndcator();
                }; break;
            case (int)SkillType.SELECT:
                {
                    EffectPerform.Instance.HideCircleIndicator();
                }; break;

            default:
                break;
        }

        UseSkill(eventData);
    }



    private void UseSkill(PointerEventData eventData)
    {
        Vector2 dir = new Vector2(eventData.position.x - originPos.x, eventData.position.y - originPos.y);

        switch (this.type)
        {
            case (int)SkillType.AOE:
                {

                };
                break;
            case (int)SkillType.DIR:
                {
                    SkillConfig.Instance.UseSkill(this.SkillId, playerCtrl.transform,
                   playerCtrl.transform.position + new Vector3(eventData.position.x - originPos.x, 0, eventData.position.y - originPos.y).normalized);

                }; break;
            case (int)SkillType.SELECT:
                {

                    SkillConfig.Instance.UseSkill(this.SkillId, playerCtrl.transform,
                     playerCtrl.transform.position + CalCircleIndicatorPos(dir));

                }; break;
            case (int)SkillType.INSTANT:
                {
                    SkillConfig.Instance.UseSkill(this.SkillId, playerCtrl.transform);
                }; break;
            default:
                break;
        }

    }

}
