using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour
{

    public GameObject damgeUiPrefab; //扣血UI预制体

    private GameObject canvas;

    public Vector3 offset;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ShowDamage(int amount)
    {
        GameObject temp = GameObject.Instantiate(damgeUiPrefab);


        temp.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + offset);
        //Debug.Log("物体转化后的屏幕坐标" + Camera.main.WorldToScreenPoint(this.transform.position));
        //Debug.Log("Text控件的屏幕坐标" + temp.transform.position);
        temp.GetComponent<Text>().text = "-" + amount.ToString();
        temp.transform.SetParent(canvas.transform);
    }

}
