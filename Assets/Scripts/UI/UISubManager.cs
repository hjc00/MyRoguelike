using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UISubManager : MonoBehaviour
{

    private Dictionary<string, GameObject> widgets;

    void Awake()
    {
        UIBase parent = transform.GetComponentInParent<UIBase>();

        UIManager.Instance.AddWidget(parent.name, gameObject.name, this.gameObject);

        widgets = new Dictionary<string, GameObject>();

        Transform[] trans = transform.GetComponentsInChildren<Transform>();

        for (int i = 0; i < trans.Length; i++)
        {
            if (trans[i].name.EndsWith("_w"))
            {
                trans[i].gameObject.AddComponent<UIWidget>();
            }
        }

    }

    public GameObject GetWidget(string widgetName)
    {
        return widgets[widgetName];
    }

    public void AddBtnClickListener(string widgetName, UnityAction action)
    {
        Button tmp = GetWidget(widgetName).GetComponent<Button>();

        if (tmp != null)
        {
            tmp.onClick.AddListener(action);
        }

    }

    void onDestroy()
    {
        widgets.Clear();
        widgets = null;
    }



}
