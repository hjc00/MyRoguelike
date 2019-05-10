using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartPanel : MonoBehaviour
{

    private Button restartBtn;
    private Button quitBtn;

    private void Awake()
    {
        restartBtn = transform.Find("restartBtn").GetComponent<Button>();
        restartBtn.onClick.AddListener(RestartGame);
        quitBtn = transform.Find("quitBtn").GetComponent<Button>();
        quitBtn.onClick.AddListener(QuitGame);
    }

    private void RestartGame()
    {
        UIManager.Instance.ClosePanel(GameDefine.restartPanel);
      //  Time.timeScale = 1;
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene("SelectScene");
    }

    private void QuitGame()
    {
        UIManager.Instance.ClosePanel(GameDefine.restartPanel);
      //  Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}
