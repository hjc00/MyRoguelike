using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        instance = this;

        allWidgets = new Dictionary<string, Dictionary<string, GameObject>>();
    }

    public GameObject GetWidget(string panelName, string widgetName)
    {
        if (allWidgets.ContainsKey(panelName))
        {
            return allWidgets[panelName][widgetName];
        }
        return null;
    }

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

}
