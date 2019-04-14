using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIndicator : MonoBehaviour
{

    private bool show = false;
    private float width;
    private Vector3 pos;  //相对位置

    private void Awake()
    {
        this.width = this.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    public void Show(Vector3 pos, float effectRange)
    {

        this.UpdatePosition();
        this.pos = pos + new Vector3(0, 0.1f, 0);
        this.transform.localScale = new Vector3(effectRange / width, effectRange / width, effectRange / width);
        show = true;
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        show = false;
        this.gameObject.SetActive(false);

    }

    private void UpdatePosition()
    {
        this.transform.position = NpcManager.Instance.Player.position + this.pos;
    }

    void Update()
    {
        if (this.show)
        {
            this.UpdatePosition();
        }
    }
}
