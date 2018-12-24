using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static JoystickController instance;
    public static JoystickController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JoystickController>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<JoystickController>();
                    DontDestroyOnLoad(obj);
                }

            }
            return instance;
        }
    }

    private Vector2 originPos;

    private float radius;

    public GameObject bounder;


    private void Start()
    {
        instance = this;

        originPos = transform.position;

        radius = Vector3.Distance(this.GetComponent<RectTransform>().position, bounder.GetComponent<RectTransform>().position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

        if (Vector3.Distance(this.transform.position, originPos) > radius)
        {

            this.transform.position = originPos + (eventData.position - originPos).normalized * radius;
        }
        else
        {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = originPos;
    }

    public float GetX()
    {
        return (int)(this.transform.position.x - originPos.x) / radius;
    }

    public float GetY()
    {
        return (int)(this.transform.position.y - originPos.y) / radius;
    }
}
