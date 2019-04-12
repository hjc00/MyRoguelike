using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;

    private Dictionary<string, Dictionary<string, GameObject>> allWidgets; //保存对所有控件的引用



    public static UIManager Instance
    {
        get
        {
            return instance;
        }

    }

    private GameObject hintPf;
    private GameObject Canvas;

    private void Awake()
    {
        instance = this;

        allWidgets = new Dictionary<string, Dictionary<string, GameObject>>();

        Canvas = GameObject.FindGameObjectWithTag("Canvas");

        hintPf = Resources.Load("Prefabs/UiHint") as GameObject;

        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetWidget(string panelName, string widgetName)
    {
        if (allWidgets.ContainsKey(panelName))
        {
            return allWidgets[panelName][widgetName];
        }
        return null;
    }

    //public GameObject GetSubManager(string panelName, string subName)
    //{
    //    if (allWidgets.ContainsKey(panelName))
    //    {
    //        return allWidgets[panelName][subName];
    //    }
    //    return null;
    //}

    public void AddWidget(string panelName, string widgetName, GameObject widgetGo)
    {
        if (!allWidgets.ContainsKey(panelName))
        {
            allWidgets[panelName] = new Dictionary<string, GameObject>();
        }
        allWidgets[panelName].Add(widgetName, widgetGo);
    }

    public void RemoveWidget(string panelName, string widgetName, GameObject widgetGo)
    {

        if (allWidgets.ContainsKey(panelName))
        {
            if (allWidgets[panelName].ContainsKey(widgetName))

                allWidgets[panelName].Remove(widgetName);

        }
    }


    public void RemovePanel(string panelName)
    {
        if (allWidgets.ContainsKey(panelName))
        {
            allWidgets[panelName].Clear();

            allWidgets[panelName] = null;
        }
    }


    public void PopHint(string str)
    {
        GameObject tempHint = Instantiate(this.hintPf);
        tempHint.transform.SetParent(this.transform);
        tempHint.transform.localPosition = Vector3.zero;
        hintPf.GetComponent<Text>().text = str;

        Destroy(tempHint, 1.2f);
    }


}
