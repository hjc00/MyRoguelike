using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectRolePanel : MonoBehaviour
{

    public Button shortRangeBtn;
    public Button longRangeBtn;

    private void Awake()
    {
        this.shortRangeBtn.onClick.AddListener(SetShortRangeRole);
        this.longRangeBtn.onClick.AddListener(SetLongRangeRole);
    }

    private void SetShortRangeRole()
    {
        PlayerPrefs.SetInt("roleType", 1);

        SceneManager.LoadScene("LoadingScene");
    }

    private void SetLongRangeRole()
    {
        PlayerPrefs.SetInt("roleType", 2);

        SceneManager.LoadScene("LoadingScene");
    }
}
