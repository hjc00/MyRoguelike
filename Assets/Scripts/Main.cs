using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    ItemUseEffectMgr ItemUseEffectMgr;
    // Use this for initialization
    void Awake()
    {
        gameObject.AddComponent<UIManager>();

        gameObject.AddComponent<NpcManager>();

        gameObject.AddComponent<ItemConfig>();

        ItemUseEffectMgr = new ItemUseEffectMgr();

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
