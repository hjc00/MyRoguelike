using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanelLogic
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("main");
    }

    public void OnLoadClick()
    {
        Debug.Log("load click");
    }

    public void OnQuitClick()
    {
        Debug.Log("quit click");
    }
}


public class StartPanel : UIBase
{
    StartPanelLogic startPanelLogic;

    void Start()
    {
        startPanelLogic = new StartPanelLogic();

        AddBtnOnClickListener("StartBtn_w", startPanelLogic.OnStartClick);
        AddBtnOnClickListener("LoadBtn_w", startPanelLogic.OnLoadClick);
        AddBtnOnClickListener("QuitBtn_w", startPanelLogic.OnQuitClick);
    }

}
