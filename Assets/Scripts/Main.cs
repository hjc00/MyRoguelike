using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    ItemUseEffectMgr ItemUseEffectMgr;
    LevelManager levelManager;
    // Use this for initialization
    void Awake()
    {

        gameObject.AddComponent<NpcManager>();

        gameObject.AddComponent<ItemConfig>();

        gameObject.AddComponent<LevelManager>();

        gameObject.AddComponent<AudioManager>();

        ItemUseEffectMgr = new ItemUseEffectMgr();

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
