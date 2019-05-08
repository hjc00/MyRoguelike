using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    static LevelManager levelManager;



    void Awake()
    {

        SkillConfig.LoadJson();
        BuffManager.LoadJson();
        RoleConfig.LoadJson();
        CastConfig.LoadJson();

        gameObject.AddComponent<NpcManager>();

        gameObject.AddComponent<ItemConfig>();

        gameObject.AddComponent<LevelManager>();

        gameObject.AddComponent<AudioManager>();


        DontDestroyOnLoad(this.gameObject);
    }

}
