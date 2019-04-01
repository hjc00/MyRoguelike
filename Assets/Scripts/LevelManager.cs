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
    private GameObject enemyPf;

    private void Awake()
    {

        instance = this;

        //todo  读取关卡配置
        bossPf = Resources.Load<GameObject>(GameDefine.RedDragonPath);
        enemyPf = Resources.Load<GameObject>(GameDefine.goblinPath);
    }

    private void Start()
    {
        SetPlayPos();
        SetBossPos();
        SetRoomEnemy();
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


        Vector3 pos = new Vector3(bossPoint.x * MapGenerator.Instance.mapCellMul, 2, bossPoint.y * MapGenerator.Instance.mapCellMul);

        GameObject bossGo = Instantiate(bossPf, pos, Quaternion.identity);

        //bossPf.transform.position = pos;
    }


    private void SetRoomEnemy()
    {
    

        for (int i = 0; i < MapGenerator.Instance.roomPoints.Count; i++)
        {
            SetSingleRoomEnemy(MapGenerator.Instance.roomPoints[i]);
        }
    }

    List<int> usedIndex = new List<int>();
    int enemyCount = 3;


    private void SetSingleRoomEnemy(List<point> singleRoomPoints)
    {
        usedIndex.Clear();
        int tempCount = 0;
        int index = Random.Range(0, singleRoomPoints.Count);

        usedIndex.Add(index);
 
        //Debug.Log(singleRoomPoints[index].x + " " + singleRoomPoints[index].y);
        while (tempCount < enemyCount)
        {

            if (usedIndex.Contains(index))
            {
                index = Random.Range(0, singleRoomPoints.Count);

            }
            else
            {
     
                usedIndex.Add(index);

                tempCount++;
            }
        }

        for (int i = 0; i < usedIndex.Count; i++)
        {

            Vector3 pos = new Vector3(singleRoomPoints[usedIndex[i]].x * MapGenerator.Instance.mapCellMul, 0, singleRoomPoints[usedIndex[i]].y * MapGenerator.Instance.mapCellMul);

            GameObject enemyGo = Instantiate(enemyPf, pos, Quaternion.identity);
        }
    }


    public void UpLevel()
    {

    }
}
