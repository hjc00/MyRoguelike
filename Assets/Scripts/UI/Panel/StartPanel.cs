using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanelLogic
{
    public void OnStartClick()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene("SelectScene");
    }

    public void OnLoadClick()
    {
        Debug.Log("load click");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}


public class StartPanel : UIBase
{
    StartPanelLogic startPanelLogic;

    void Start()
    {
        startPanelLogic = new StartPanelLogic();

        AddBtnOnClickListener("StartBtn_w", startPanelLogic.OnStartClick);
      //  AddBtnOnClickListener("LoadBtn_w", startPanelLogic.OnLoadClick);
        AddBtnOnClickListener("QuitBtn_w", startPanelLogic.OnQuitClick);
    }

}
