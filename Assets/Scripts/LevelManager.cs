using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private GameObject skillSellNpcPf;
    private GameObject itemSellNpcPf;

    private GameObject playerPf;

    private void Awake()
    {

        instance = this;

        //todo  读取关卡配置
        bossPf = Resources.Load<GameObject>(GameDefine.RedDragonPath);
        enemyPf = Resources.Load<GameObject>(GameDefine.goblinPath);

        skillSellNpcPf = Resources.Load<GameObject>(GameDefine.skillSellNpcPath);
        itemSellNpcPf = Resources.Load<GameObject>(GameDefine.itemSellNpcPath);
        playerPf = Resources.Load<GameObject>(GameDefine.playerPfPath);

        EventCenter.AddListener(EventType.OnBossDie, ReloadLevel);

    }


    public void StartLevel()
    {
        SetBossPos();
        SetRoom();
        SetPlayPos();
        EventCenter.Broadcast<int>(EventType.OnUpdateGold, 0); ;
        // if (UIManager.Instance != null)
        //  Debug.Log("cur level " + PlayerPrefs.GetInt("level"));
        UIManager.Instance.PopHint(string.Format("level {0}", PlayerPrefs.GetInt("level")));
    }

    public int Level { get; set; }

    public void SetPlayPos()
    {
        point playerPoint = MapGenerator.Instance.playerPoint;


        GameObject player = Instantiate(this.playerPf);
        NpcManager.Instance.Player = player.transform;
        player.transform.position = new Vector3(playerPoint.x * MapGenerator.Instance.mapCellMul, 0, playerPoint.y * MapGenerator.Instance.mapCellMul);


    }

    public void SetBossPos()
    {
        point bossPoint = MapGenerator.Instance.bossPoint;


        Vector3 pos = new Vector3(bossPoint.x * MapGenerator.Instance.mapCellMul, 2, bossPoint.y * MapGenerator.Instance.mapCellMul);

        GameObject bossGo = Instantiate(bossPf, pos, Quaternion.identity);

        //bossPf.transform.position = pos;
    }

    int itemRoomCnt = 0;
    int skillRoomCnt = 0;

    private void SetRoom()
    {
        int random = 0;

        for (int i = 0; i < MapGenerator.Instance.roomPoints.Count; i++)
        {
            random = Random.Range(0, 300);

            if (random < 100 && itemRoomCnt < 1 && i > 0)
            {
                SetItemRoom(i);
            }
            else if (random >= 100 && random < 200 && skillRoomCnt < 1 && i > 0)
            {
                SetSkillRoom(i);
            }
            else if (i > 0)
            {
                SetSingleRoomEnemy(MapGenerator.Instance.roomPoints[i]);
            }
        }
    }

    List<int> usedIndex = new List<int>();
    int enemyCount = 3;


    private void SetSingleRoomEnemy(List<point> singleRoomPoints)
    {
        if (singleRoomPoints.Contains(MapGenerator.Instance.bossPoint))
        {
            return;
        }

        usedIndex.Clear();
        int tempCount = 1;
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

    private void SetItemRoom(int roomCenterIndex)
    {
        if (MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].x == MapGenerator.Instance.bossPoint.x
            ||
            MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].y == MapGenerator.Instance.bossPoint.y)
            return;

        Vector3 pos = new Vector3(MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].x * MapGenerator.Instance.mapCellMul, 2, MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].y * MapGenerator.Instance.mapCellMul);
        GameObject itemSellNpc = Instantiate(itemSellNpcPf, pos, Quaternion.identity);
        itemRoomCnt++;
    }

    private void SetSkillRoom(int roomCenterIndex)
    {
        if (MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].x == MapGenerator.Instance.bossPoint.x
         ||
         MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].y == MapGenerator.Instance.bossPoint.y)
            return;

        Vector3 pos = new Vector3(MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].x * MapGenerator.Instance.mapCellMul, 2, MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].y * MapGenerator.Instance.mapCellMul);
        GameObject skillSellNpc = Instantiate(skillSellNpcPf, pos, Quaternion.identity);
        skillRoomCnt++;
    }

    public void ReloadLevel()
    {
        int curLevel = PlayerPrefs.GetInt("level");

        curLevel++;
        if (curLevel > 3)
        {
            UIManager.Instance.PopHint("恭喜成功通关！");
            PlayerPrefs.SetInt("level", curLevel);
            StartCoroutine(BackToMainMenu());
        }
        else
        {
            PlayerPrefs.SetInt("level", curLevel);
            StartCoroutine(StartReloading());
        }
    }

    IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator StartReloading()
    {

        yield return new WaitForSeconds(3);
        NpcManager.Instance.ClearAllEnemy();
        SceneManager.LoadScene("LoadingScene");
    }
}
