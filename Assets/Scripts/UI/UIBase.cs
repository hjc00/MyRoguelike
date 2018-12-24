using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UIBase : MonoBehaviour
{

    private void Awake()
    {
        Transform[] trans = transform.GetComponentsInChildren<Transform>();

        for (int i = 0; i < trans.Length; i++)
        {
            if (trans[i].name.EndsWith("_w"))
                trans[i].gameObject.AddComponent<UIWidget>();
        }
    }

    public GameObject GetWidget(string widgetName)
    {
        return UIManager.Instance.GetWidget(this.gameObject.name, widgetName);
    }

    public UIWidget GetUIWidget(string widgetName)
    {
        GameObject go = GetWidget(widgetName);

        if (go != null)
        {
            return go.GetComponent<UIWidget>();
        }

        return null;
    }

    public void AddBtnOnClickListener(string widgetName, UnityAction action)
    {
        UIWidget tmp = GetUIWidget(widgetName);

        if (tmp != null)
        {
            tmp.AddBtnClickListener(action);
        }
    }

    private void OnDestroy()
    {
        UIManager.Instance.RemovePanel(this.gameObject.name);
    }
}
