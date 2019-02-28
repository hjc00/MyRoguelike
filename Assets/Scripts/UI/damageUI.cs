using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageUI : MonoBehaviour
{

    public float speed = 500f;
    public float time = 1f;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, time + 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

}
