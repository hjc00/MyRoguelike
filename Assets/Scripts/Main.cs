using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    static LevelManager levelManager;

    static bool hasCreated = false;

    void Awake()
    {
        if (hasCreated)
            return;


        SkillConfig.LoadJson();
        BuffManager.LoadJson();
        RoleConfig.LoadJson();
        CastConfig.LoadJson();

        gameObject.AddComponent<NpcManager>();

        gameObject.AddComponent<ItemConfig>();

        gameObject.AddComponent<LevelManager>();

        gameObject.AddComponent<AudioManager>();

        hasCreated = true;
        DontDestroyOnLoad(this.gameObject);
    }

}
