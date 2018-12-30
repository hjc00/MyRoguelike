using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapModel
{
    int[,] coordinates;  //坐标点

    public int[,] Coordinates
    {
        get { return coordinates; }
    }

    int width;  //宽度

    public int Width
    {
        get { return width; }
    }

    int length; //长度

    public int Length
    {
        get { return length; }
    }

    int roomCount; //房间数量

    public int RoomCount
    {
        get { return roomCount; }
    }

    public MapModel(int _length, int _width, int _roomCount)
    {
        this.length = _length;
        this.width = _width;
        this.coordinates = new int[_length, _width];
        this.roomCount = _roomCount;
        //InitCoordinate();
    }

    private void InitCoordinate()
    {
        for (int i = 0; i < this.length; i++)
        {
            for (int j = 0; j < this.width; j++)
            {
                this.coordinates[i, j] = 0;
            }
        }
    }
};


public class MapGenerator : MonoBehaviour
{
    MapModel map;

    public GameObject cubePf;

    private List<Vector3> points = new List<Vector3>();

    void Awake()
    {
        map = new MapModel(50, 50, 5);
    }

    void Start()
    {
        CreateRoomFlow();
    }

    void Update()
    {

    }

    void CreateRoomFlow()   //创建房间流程
    {
        RoomModel firstRoom = CreateRoom(Vector3.zero, 3, 3);  //在原点创建第一个房间

        points.Add(firstRoom.Center);

        int curRoomCount = 1;

        Vector3 lastCenter = firstRoom.Center;

        Vector3 nextCenter = Vector3.zero;

        while (curRoomCount < map.RoomCount)
        {
            int nextLengh = Random.Range(10,15); //可随机

            int nextWidth = Random.Range(10, 15); //可随机

            nextCenter = FindNextRoomCenter(lastCenter, nextLengh, nextWidth);


            while (!CheckSpaceEnough(nextCenter, nextLengh, nextWidth))
            {
                nextCenter = FindNextRoomCenter(lastCenter, nextLengh, nextWidth);
            }

            lastCenter = nextCenter;

            points.Add(nextCenter);

            CreateRoom(nextCenter, nextLengh, nextWidth);

            curRoomCount++;
        }
    }
    RoomModel CreateRoom(Vector3 _center, int _long, int _width)
    {
        RoomModel room = new RoomModel(_center, _long, _width);

        int startX = (int)(room.Center.x - room.Width * 0.5f);  //循环开始的索引

        int startZ = (int)(room.Center.z - room.Length * 0.5f);  //

        for (int i = 0; i < room.Width - 1; i++)
        {
            for (int j = 0; j < room.Length - 1; j++)
            {
                GameObject cubeGo = Instantiate(cubePf);

                cubeGo.transform.position = new Vector3(startX + i, 0, startZ + j);
            }
        }
        return room;
    }

    void CreatePath()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_lastCenter">  上一个房间的中心点   </param>
    /// <param name="_nextLength">  下一个房间的长度    </param>
    /// <param name="_nextWidth">   下一个房间的宽度    </param>
    /// <returns></returns>
    Vector3 FindNextRoomCenter(Vector3 _lastCenter, int _nextLength, int _nextWidth)        //寻找下一个房间的原点
    {
        Vector3 nextCenter = _lastCenter;

        int tmpDir = Random.Range((int)RoomDir.Left, (int)RoomDir.Bottom + 1);

        Debug.Log("tmpDir " + (RoomDir)tmpDir);

        int pathLength = Random.Range(15, 30);   //随机出房间通路的长度，此处先固定，以后可扩展

        //Debug.Log("pathLength " + pathLength);

        if (tmpDir == (int)RoomDir.Left)
        {
            nextCenter.x -= (pathLength + _nextWidth * 0.5f);
        }
        else if (tmpDir == (int)RoomDir.Right)
        {
            nextCenter.x += (pathLength - _nextWidth * 0.5f);
        }
        else if (tmpDir == (int)RoomDir.Top)
        {
            nextCenter.z += (pathLength + _nextLength * 0.5f);
        }
        else if (tmpDir == (int)RoomDir.Bottom)
        {
            nextCenter.z -= (pathLength - _nextLength * 0.5f);
        }
        return nextCenter;
    }

    bool CheckSpaceEnough(Vector3 _nextCenter, int _nextLengh, int _nextWidth)
    {

        if (Physics.CheckBox(_nextCenter, new Vector3(_nextLengh, 10, _nextWidth)))
        {
            return false;
        }
        else
        {
            return true;
        }

    }


    void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(points[i], i + 1);
        }
    }
}
