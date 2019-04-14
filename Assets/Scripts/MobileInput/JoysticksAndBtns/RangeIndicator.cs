using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{

    private bool show = false;
    private float width;

    private void Awake()
    {
        this.width = this.GetComponent<SpriteRenderer>().bounds.size.x;

    }

    public void Show(float range)
    {

        this.UpdatePosition();
        show = true;

        this.transform.localScale = new Vector3(range / width, range / width, range / width);
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
    }

    void Update()
    {
        if (this.show)
        {
            this.UpdatePosition();
        }
    }
}
