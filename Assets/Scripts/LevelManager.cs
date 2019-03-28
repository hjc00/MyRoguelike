using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int maxLevel = 3;
    public int curLevel = 1;

    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
    }

    private GameObject bossPf;

    private void Awake()
    {

        instance = this;

        //todo  读取关卡配置
        bossPf = Resources.Load<GameObject>(GameDefine.RedDragonPath);
    }

    private void Start()
    {
        SetPlayPos();
        SetBossPos();
    }


    public int Level { get; set; }

    public void SetPlayPos()
    {
        point playerPoint = MapGenerator.Instance.playerPoint;

        NpcManager.Instance.Player.transform.position = new Vector3(playerPoint.x * MapGenerator.Instance.mapCellMul, 0, playerPoint.y * MapGenerator.Instance.mapCellMul);
    }

    public void SetBossPos()
    {
        point bossPoint = MapGenerator.Instance.bossPoint;

        Debug.Log(bossPoint.x);
        Debug.Log(bossPoint.y);

        Vector3 pos = new Vector3(bossPoint.x * MapGenerator.Instance.mapCellMul, 2, bossPoint.y * MapGenerator.Instance.mapCellMul);
        Debug.Log(pos);

        GameObject bossGo = Instantiate(bossPf, pos, Quaternion.identity);

        //bossPf.transform.position = pos;
    }

    public void UpLevel()
    {

    }
}
