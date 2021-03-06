﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        EventCenter.AddListener<int>(EventType.OnUpdateGold, UpdateText);

    }

    private void UpdateText(int amount)
    {
        this.gameObject.SetActive(true);
        text.text = string.Format("Gold:{0}", amount);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventType.OnUpdateGold, UpdateText);
    }
}
