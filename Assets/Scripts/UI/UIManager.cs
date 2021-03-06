﻿using System.Collections;
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
            //if (instance == null)
            //{
            //    GameObject obj = new GameObject();
            //    instance = obj.AddComponent<UIManager>();
            //    obj.name = "UICanvas";
            //    DontDestroyOnLoad(obj);
            //}
            return instance;
        }

    }

    private GameObject hintPf;
    private GameObject Canvas;

    private static bool hasCreated = false;

    private void Awake()
    {
        // if (hasCreated == false)
        //  {

        DontDestroyOnLoad(gameObject);

        // }

        instance = this;

        allWidgets = new Dictionary<string, Dictionary<string, GameObject>>();

        Canvas = this.gameObject;

        hintPf = Resources.Load("Prefabs/UiHint") as GameObject;

        hasCreated = true;

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

    private GameObject hintGo;

    public void PopHint(string str)
    {
        if (hintGo == null)
        {
            hintGo = Instantiate(this.hintPf);
        }
        hintGo.GetComponentInChildren<Text>().text = str;

        hintGo.transform.SetParent(this.transform);
        hintGo.transform.localPosition = Vector3.zero;

        Destroy(hintGo, 1.2f);
    }


    private Dictionary<string, GameObject> panelDict = new Dictionary<string, GameObject>();

    public GameObject PopPanel(string name, TweenCallback tweenCallback)
    {
        GameObject panel = null;
        panelDict.TryGetValue(name, out panel);

        if (panel == null)
        {
            panel = Instantiate(Resources.Load(GameDefine.panelPath + name)) as GameObject;
            panel.transform.SetParent(this.Canvas.transform);
            panel.transform.localPosition = Vector3.zero;
            panelDict.Add(name, panel);

        }
        else
        {
            panel.SetActive(true);
        }

        Tween tween = panel.transform.DOScale(1, 0.5f);
        tween.OnComplete(tweenCallback);

        return panel;
    }

    public GameObject PopPanel(string name)
    {
        GameObject panel = null;
        panelDict.TryGetValue(name, out panel);

        if (panel == null)
        {
            panel = Instantiate(Resources.Load(GameDefine.panelPath + name)) as GameObject;
            panel.transform.SetParent(this.Canvas.transform);
            panel.transform.localPosition = Vector3.zero;
            panelDict.Add(name, panel);
        }
        else
        {
            panel.SetActive(true);
        }
        Tween tween = panel.transform.DOScale(1, 0.5f);

        return panel;

    }

    public void DestroyAllPanel()
    {
        foreach (KeyValuePair<string, GameObject> panels in panelDict)
        {
            Destroy(panels.Value);

        }
        panelDict.Clear();
    }


    public void ClosePanel(string name)
    {
        GameObject panel = null;
        panelDict.TryGetValue(name, out panel);

        if (panel == null)
            return;
        else
        {
            panel.transform.DOScale(0, 0.5f).OnComplete(() =>
            {
                panel.SetActive(false);
            });

        }

    }

    public void ClosePanel(string name, TweenCallback tweenCallback)
    {
        GameObject panel = null;
        panelDict.TryGetValue(name, out panel);

        if (panel == null)
            return;
        else
        {
            panel.transform.DOScale(0, 0.5f).OnComplete(() =>
            {

                tweenCallback();
                panel.SetActive(false);
            });

        }

    }
}
