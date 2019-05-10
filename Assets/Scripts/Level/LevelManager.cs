using Newtonsoft.Json;
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

    private static Dictionary<int, LevelInfo> levelDict = new Dictionary<int, LevelInfo>();

    private GameObject bossPf;
    private GameObject enemyPf;
    private GameObject skillSellNpcPf;
    private GameObject itemSellNpcPf;

    private GameObject shortRangeRolePf;
    private GameObject longRangeRolePf;

    private int roleType;

    private static void LoadJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/Level");

        LevelInfo[] levelInfos = JsonConvert.DeserializeObject<LevelInfo[]>(textAsset.text);

        for (int i = 0; i < levelInfos.Length; i++)
        {
            levelDict.Add(levelInfos[i].id, levelInfos[i]);
        }

        System.Array.Clear(levelInfos, 0, levelInfos.Length);
        levelInfos = null;
    }

    public LevelInfo GetCurLevelInfo()
    {
        LevelInfo temp = null;
        levelDict.TryGetValue(PlayerPrefs.GetInt("level"), out temp);
        if (temp == null)
        {
            Debug.Log("levelInfo 不存在！" + PlayerPrefs.GetInt("level"));
        }
        return temp;
    }

    private void Awake()
    {

        instance = this;


        skillSellNpcPf = Resources.Load<GameObject>(GameDefine.skillSellNpcPath);
        itemSellNpcPf = Resources.Load<GameObject>(GameDefine.itemSellNpcPath);

        shortRangeRolePf = Resources.Load<GameObject>(GameDefine.shortRangeRole);
        longRangeRolePf = Resources.Load<GameObject>(GameDefine.longRangeRole);

        EventCenter.AddListener(EventType.OnBossDie, ReloadLevel);

        LoadJson();
    }

    public void LoadBossPf()  //读取该关卡对应的boss
    {
        bossPf = Resources.Load<GameObject>(GameDefine.rolePath + GetCurLevelInfo().bossPf);
    }

    public void LoadEnemyBoss()  //读取该关卡对应的小怪
    {
        enemyPf = Resources.Load<GameObject>(GameDefine.rolePath + GetCurLevelInfo().enemyPf);
    }


    public void StartLevel()
    {
        roleType = PlayerPrefs.GetInt("roleType");
        LoadBossPf();
        LoadEnemyBoss();
        SetPlayPos();
        SetBossPos();
        SetRoom();
        EventCenter.Broadcast<int>(EventType.OnUpdateGold, 0); ;
        // if (UIManager.Instance != null)
        //  Debug.Log("cur level " + PlayerPrefs.GetInt("level"));
        UIManager.Instance.PopHint(string.Format("level {0}", PlayerPrefs.GetInt("level")));
    }

    public int Level { get; set; }

    public void SetPlayPos()
    {
        point playerPoint = MapGenerator.Instance.playerPoint;
        GameObject player = null;

        if (this.roleType == 1)
        {
            player = Instantiate(this.shortRangeRolePf);
        }
        else if (this.roleType == 2)
        {

            player = Instantiate(this.longRangeRolePf);

        }

        player.AddComponent<PlayerSkill>();
        NpcManager.Instance.Player = player.transform;

        player.transform.position = new Vector3(playerPoint.x * MapGenerator.Instance.mapCellMul, 0, playerPoint.y * MapGenerator.Instance.mapCellMul);


    }

    public void SetBossPos()
    {
        point bossPoint = MapGenerator.Instance.bossPoint;


        Vector3 pos = new Vector3(bossPoint.x * MapGenerator.Instance.mapCellMul, 0.01f, bossPoint.y * MapGenerator.Instance.mapCellMul);

        GameObject bossGo = Instantiate(bossPf, pos, Quaternion.identity);

        //bossPf.transform.position = pos;
    }

    int itemRoomCnt = 0;
    int skillRoomCnt = 0;

    private void SetRoom()
    {
        int random = 0;
        SetSkillRoom(1);
        for (int i = 1; i < MapGenerator.Instance.roomPoints.Count; i++)
        {
            random = Random.Range(0, 300);

            // SetSkillRoom(i);
            SetSingleRoomEnemy(MapGenerator.Instance.roomPoints[i]);
            //if (random < 100 && itemRoomCnt < 1 && i > 0)
            //{
            //    SetItemRoom(i);
            //}
            //else if (random >= 100 && random < 200 && skillRoomCnt < 1 && i > 0)
            //{
            //   SetSkillRoom(i);
            //}
            //else if (i > 0)
            //{
            //    SetSingleRoomEnemy(MapGenerator.Instance.roomPoints[i]);
            //}
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

        // Vector3 pos = new Vector3(MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].x * MapGenerator.Instance.mapCellMul, 2, MapGenerator.Instance.RoomCenterPoints[roomCenterIndex].y * MapGenerator.Instance.mapCellMul);
        Vector3 pos = NpcManager.Instance.Player.position + new Vector3(2, 0, 2);
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
