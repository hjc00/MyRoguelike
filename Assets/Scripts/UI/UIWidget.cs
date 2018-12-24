using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIWidget : MonoBehaviour
{

    private void Awake()
    {
        UIBase parent = transform.GetComponentInParent<UIBase>();

        UIManager.Instance.AddWidget(parent.transform.name, this.gameObject.name, this.gameObject);

    }

    public void AddBtnClickListener(UnityAction action)
    {
        Button tmp = transform.GetComponent<Button>();

        if (tmp != null)
        {
            tmp.onClick.AddListener(action);
        }

    }
}
