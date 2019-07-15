using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum mapEnum
{
    Floor = 0,
    LeftWall = 1,
    RightWall = 2,
    UpWall = 3,
    BottomWall = 4,
    Door = 5,
    Path = 6,
    PathWall = 7,
    ItemNpc = 8,
    SkillNpc = 9,
    Enemy = 10,
    Boss = 11,
}

public class point
{
    public int x;
    public int y;

    public point()
    {
        x = 0;
        y = 0;
    }

    public point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public point(float x, float y)
    {
        this.x = (int)x;
        this.y = (int)y;
    }
}

public class MapGenerator : MonoBehaviour
{


    private int[,] map;

    public int[,] Map
    {
        get { return map; }
    }

    private static MapGenerator instance;
    public static MapGenerator Instance
    {
        get { return instance; }
    }

    public int mapWidth = 100;
    public int mapLenghth = 100;
    public int roomMinWidth = 5;
    public int roomMaxWidth = 8;
    public int maxTryCount = 100;
    public int minPathLength = 8;
    public int maxPathLength = 10;
    public int roomCount = 6;

    private GameObject floorPf;
    private GameObject wallPf;
    private GameObject doorPf;
    private GameObject pathPf;

    private List<point> points = new List<point>();
    private List<point> wallPoints = new List<point>();
    private List<point> floorPoints = new List<point>();
    private List<point> roomCenterPoints = new List<point>();

    public List<List<point>> roomPoints { get; private set; }

    public List<point> RoomCenterPoints
    {
        get { return this.roomCenterPoints; }
    }

    public point playerPoint { get; private set; }   //玩家起始位置
    public point bossPoint { get; private set; }   //玩家起始位置

    public int mapCellMul = 5;  //地图元素在游戏场景的实际偏移位置

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;

        LevelInfo tempLevel = LevelManager.Instance.GetCurLevelInfo();

        this.mapWidth = tempLevel.width;
        this.mapLenghth = tempLevel.length;
        this.roomMinWidth = tempLevel.roomMinWidth;
        this.roomMaxWidth = tempLevel.roomMaxWidth;
        this.minPathLength = tempLevel.pathMinLength;
        this.maxPathLength = tempLevel.pathMaxLength;
        this.roomCount = tempLevel.maxRoomCnt;




        floorPf = Resources.Load<GameObject>("Prefabs/Level/" + tempLevel.floorPf);
        wallPf = Resources.Load<GameObject>("Prefabs/Level/" + tempLevel.wallPf);
        doorPf = Resources.Load<GameObject>("Prefabs/Level/" + tempLevel.wallPf);
        pathPf = Resources.Load<GameObject>("Prefabs/Level/" + tempLevel.pathPf);

        bossPoint = new point();
        roomPoints = new List<List<point>>();

        map = new int[mapLenghth, mapWidth];
        InitMap();
        CreateFirstRoom();
    }

    void Start()
    {
        DrawMap();
    }

    bool IsOutY(int y)
    {

        if ((y < 0) || (y > this.mapLenghth - 1))
            return true;
        else
            return false;
    }

    bool isOutX(int x)
    {

        if ((x < 0) || (x > this.mapWidth - 1))
            return true;
        else
            return false;
    }

    bool isAreaUsed(int xStart, int yStart, int xEnd, int yEnd)
    {
        //  Debug.Log("checked used");
        for (int i = xStart; i <= xEnd; i++)
            for (int j = yStart; j <= yEnd; j++)
                if (map[i, j] != -1)
                    return true;
        return false;
    }

    void CreateFirstRoom()   //创建第一个房间
    {
        int startX = (int)Random.Range(mapWidth * 0.25f, mapWidth * 0.75f);
        int startY = (int)Random.Range(mapLenghth * 0.25f, mapLenghth * 0.75f);

        map[startX, startY] = (int)mapEnum.Floor;

        int width = Random.Range(roomMinWidth, roomMaxWidth);
        int length = Random.Range(roomMinWidth, roomMaxWidth);

        int endX = startX + width;
        int endY = startY + length;

        SetRoomEnum(startX, startY, endX, endY);

        playerPoint = new point((startX + endX) * 0.5f, (startY + endY) * 0.5f);

        CreateRoom();

    }


    void SetRoomEnum(int startX, int startY, int endX, int endY)
    {
        List<point> tmpRoom = new List<point>();

        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                map[i, j] = (int)mapEnum.Floor;

                if (i == startX)
                {
                    map[i, j] = (int)mapEnum.LeftWall;
                }

                else if (i == endX)
                {
                    map[i, j] = (int)mapEnum.RightWall;
                }

                else if (j == startY)
                {
                    map[i, j] = (int)mapEnum.BottomWall;
                }

                else if (j == endY)
                {
                    map[i, j] = (int)mapEnum.UpWall;
                }

                if (map[i, j] != (int)mapEnum.Floor)
                {
                    if (i > startX && i < endX
                    || j > startY && j < endY)
                        wallPoints.Add(new point(i, j));
                }

                if (map[i, j] == (int)mapEnum.Floor)
                    tmpRoom.Add(new point(i, j));
            }
        }

        point center = new point((startX + endX) * 0.5f, (startY + endY) * 0.5f);

        if (center.x != 0 && center.y != 0)
        {
            roomPoints.Add(tmpRoom);
            roomCenterPoints.Add(center);

            // SetRoomEnum(tmpRoom, center);
        }

    }

    //List<int> usedIndex = new List<int>();
    //int enemyCount = 3;
    //int itemRoomCnt = 0;
    //int skillRoomCnt = 0;

    //private void SetRoomEnum(List<point> roomPoints, point center)
    //{
    //    int random = 0;


    //    random = Random.Range(0, 300);

    //    if (random < 100 && itemRoomCnt < 1)
    //    {
    //        map[center.x, center.y] = (int)mapEnum.ItemNpc;
    //        itemRoomCnt++;
    //    }
    //    else if (random >= 100 && random < 200 && skillRoomCnt < 1)
    //    {
    //        map[center.x, center.y] = (int)mapEnum.SkillNpc;
    //        skillRoomCnt++;
    //    }
    //    else
    //    {
    //        SetSingleRoomEnemy();
    //    }

    //}

    //private void SetSingleRoomEnemy()
    //{

    //}


    void SetPathEnum(int xStart, int yStart, int xEnd, int yEnd, int dir)
    {
        for (int i = xStart; i <= xEnd; i++)
        {
            for (int j = yStart; j <= yEnd; j++)
            {
                map[i, j] = (int)mapEnum.Path;
            }
        }

        if (dir == 1 || dir == 2)
        {
            for (int i = xStart; i <= xEnd; i++)
            {
                map[i, yStart] = (int)mapEnum.PathWall;
            }
            for (int i = xStart; i <= xEnd; i++)
            {
                map[i, yEnd] = (int)mapEnum.PathWall;
            }
        }
        else if (dir == 3 || dir == 4)
        {
            for (int i = yStart; i <= yEnd; i++)
            {
                map[xStart, i] = (int)mapEnum.PathWall;
            }
            for (int i = yStart; i <= yEnd; i++)
            {
                map[xEnd, i] = (int)mapEnum.PathWall;
            }
        }


    }

    void SetBossPoint()
    {
        int maxSqrDis = 0;

        for (int i = 0; i < this.roomCenterPoints.Count; i++)
        {
            int x = this.roomCenterPoints[i].x;
            int y = this.roomCenterPoints[i].y;

            int sqrDis = x * x + y * y;
            if (sqrDis > maxSqrDis)
            {
                bossPoint.x = x;
                bossPoint.y = y;
            }
        }

    }

    bool CheckPathEnough()
    {
        return false;
    }

    bool CheckUnused()
    {
        return false;
    }

    void CreateRoom()
    {
        int roomNum = 1;
        for (int i = 0; i < maxTryCount; i++)
        {
            int index = Random.Range(0, wallPoints.Count);

            point p = wallPoints[index];

            int width = Random.Range(roomMinWidth, roomMaxWidth);
            int length = Random.Range(roomMinWidth, roomMaxWidth);

            int pathLength = Random.Range(minPathLength, maxPathLength);


            int startX = 0, endX = 0, startY = 0, endY = 0;

            int pathStartX = 0, pathStartY = 0, pathEndX = 0, pathEndY = 0;
            int dir = 0;

            switch (map[p.x, p.y])
            {

                case (int)mapEnum.LeftWall:
                    {
                        startX = p.x - pathLength - width;
                        endX = p.x - pathLength;
                        startY = p.y - length / 2;
                        endY = p.y + length / 2;

                        pathStartX = p.x - pathLength;
                        pathEndX = p.x;
                        pathStartY = p.y - 1;
                        pathEndY = p.y + 1;

                        dir = 1;
                    }; break;
                case (int)mapEnum.RightWall:
                    {
                        startX = p.x + 1 + pathLength;
                        endX = startX + pathLength + width;
                        startY = p.y - length / 2;
                        endY = p.y + length / 2;

                        pathStartX = p.x;
                        pathEndX = p.x + pathLength + 1;
                        pathStartY = p.y - 1;
                        pathEndY = p.y + 1;

                        dir = 2;

                    }; break;
                case (int)mapEnum.UpWall:
                    {

                        startX = p.x - width / 2;
                        endX = p.x + width / 2;
                        startY = p.y + 1 + pathLength;
                        endY = p.y + 1 + pathLength + length;

                        pathStartX = p.x - 1;
                        pathEndX = p.x + 1;
                        pathStartY = p.y;
                        pathEndY = p.y + pathLength + 1;

                        dir = 3;

                    }; break;
                case (int)mapEnum.BottomWall:
                    {

                        startX = p.x - width / 2;
                        endX = p.x + width / 2;
                        startY = p.y - pathLength - length;
                        endY = p.y - pathLength;

                        pathStartX = p.x - 1;
                        pathEndX = p.x + 1;
                        pathStartY = p.y - pathLength;
                        pathEndY = p.y;

                        dir = 4;
                    }; break;

            }
            if (!isOutX(startX) && !IsOutY(startY) && !isOutX(endX) && !IsOutY(endY) &&
                  !isOutX(startX) && !IsOutY(startY) && !isOutX(endX) && !IsOutY(endY))
            {

                if (!isAreaUsed(startX, startY, endX, endY))
                {
                    SetRoomEnum(startX, startY, endX, endY);
                    SetPathEnum(pathStartX, pathStartY, pathEndX, pathEndY, dir);
                    roomNum++;
                    if (roomNum == roomCount)
                    {
                        break;
                    }
                }
            }

        }

        SetBossPoint();
        if (roomNum < roomCount)
        {
            Debug.Log("无法生成指定个数的房间！请确认数据的合法性或加大步数");
        }
        LevelManager.Instance.StartLevel();
    }

    void InitMap()
    {
        for (int i = 0; i < mapLenghth; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                map[i, j] = -1;
            }
        }
    }


    private void DrawMap()
    {
        for (int i = 0; i < mapLenghth; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                switch (map[i, j])
                {
                    case (int)mapEnum.Floor:
                        {

                            GameObject go = Instantiate(floorPf);
                            go.transform.position = new Vector3(i * mapCellMul, 0, j * mapCellMul);
                        }; break;
                    case (int)mapEnum.LeftWall:
                        {
                            GameObject go = Instantiate(wallPf);
                            go.transform.position = new Vector3(i * mapCellMul, 0, j * mapCellMul);
                        }; break;
                    case (int)mapEnum.RightWall:
                        {
                            GameObject go = Instantiate(wallPf);
                            go.transform.position = new Vector3(i * mapCellMul, 0, j * mapCellMul);

                        }; break;
                    case (int)mapEnum.UpWall:
                        {
                            GameObject go = Instantiate(wallPf);
                            go.transform.position = new Vector3(i * mapCellMul, 0, j * mapCellMul);

                        }; break;
                    case (int)mapEnum.BottomWall:
                        {
                            GameObject go = Instantiate(wallPf);
                            go.transform.position = new Vector3(i * mapCellMul, 0, j * mapCellMul);
                        }; break;
                    case (int)mapEnum.Door:
                        {
                            GameObject go = Instantiate(doorPf);
                            go.transform.position = new Vector3(i * mapCellMul, 0, j * mapCellMul);
                        }; break;
                    case (int)mapEnum.Path:
                        {
                            GameObject go = Instantiate(pathPf);
                            go.transform.position = new Vector3(i * mapCellMul, 0, j * mapCellMul);
                        }; break;
                    case (int)mapEnum.PathWall:
                        {
                            GameObject go = Instantiate(wallPf);
                            go.transform.position = new Vector3(i * mapCellMul, 0, j * mapCellMul);
                        }; break;
                }
            }
        }
    }

}
