using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        gameObject.AddComponent<UIManager>();

        gameObject.AddComponent<NpcManager>();

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
