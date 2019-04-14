using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{

    private bool show = false;
    private float width;
    private float angle;

    private void Awake()
    {
        this.width = this.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    public void Show(float range, float angle)
    {

        this.UpdatePosition();
        show = true;

        this.transform.localScale = new Vector3(range / width, 1, range / width);
        this.angle = angle;
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        show = false;
        this.gameObject.SetActive(false);

    }

    private void UpdatePosition()
    {
        this.transform.position = NpcManager.Instance.Player.position + new Vector3(0, 0.1f, 0);
        this.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    void Update()
    {
        if (this.show)
        {
            this.UpdatePosition();
        }
    }
}
