using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class PausePanel : BasePanel
{

    private Button contineBtn;
    private Button quitBtn;
    private Button backToMenuBtn;

    TweenCallback onClosePausePanel;

    public override void Awake()
    {

        contineBtn = transform.Find("continueBtn").GetComponent<Button>();
        quitBtn = transform.Find("quitBtn").GetComponent<Button>();
        backToMenuBtn = transform.Find("backToMenuBtn").GetComponent<Button>();

        contineBtn.onClick.AddListener(OnContinueBtnClick);
        quitBtn.onClick.AddListener(OnQuitBtnClick);
        backToMenuBtn.onClick.AddListener(OnBackToMenuBtnClick);

        onClosePausePanel += ResumeTimeScale;
    }

    private void ResumeTimeScale()
    {
        Time.timeScale = 1;
    }

    private void OnContinueBtnClick()
    {
        ResumeTimeScale();
        UIManager.Instance.ClosePanel(GameDefine.pausePanel, onClosePausePanel);
    }

    private void OnQuitBtnClick()
    {
        Application.Quit();
    }

    private void OnBackToMenuBtnClick()
    {
        ResumeTimeScale();
        UIManager.Instance.ClosePanel(GameDefine.pausePanel, onClosePausePanel);
        SceneManager.LoadScene("StartScene");
    }

    private void OnDestroy()
    {
        onClosePausePanel -= ResumeTimeScale;
    }
}
