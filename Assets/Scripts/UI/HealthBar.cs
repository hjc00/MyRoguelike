using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;


    private void Start()
    {
        NpcManager.Instance.Player.GetComponent<PlayerCtrl>().onPlayerHealthReduce += UpdateHealthBar;
    }

    private void UpdateHealthBar(int healthAmount)
    {

        healthSlider.value = healthAmount;
    }
}
