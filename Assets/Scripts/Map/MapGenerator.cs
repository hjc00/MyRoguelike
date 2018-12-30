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

    public GameObject pathPf;

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
        //RoomModel lastRoom = CreateRoom(Vector3.zero, 5, 5);  //在原点创建第一个房间

        //points.Add(lastRoom.Center);

        //int curRoomCount = 1;

        int curRoomCount = 0;

        //Vector3 lastCenter = lastRoom.Center;

        RoomModel lastRoom = new RoomModel();

        Vector3 nextCenter = Vector3.zero;

        Vector3 lastCenter = Vector3.zero;

        int count = 0;
        while (curRoomCount < map.RoomCount || count > 5000)
        {
            count++;

            int nextLengh = Random.Range(10, 15); //可随机

            int nextWidth = Random.Range(10, 15); //可随机

            int pathLength = Random.Range(15, 30);   //随机出房间通路的长度，此处先固定，以后可扩展

            int dir = Random.Range((int)RoomDir.Left, (int)RoomDir.Bottom + 1);   //随机生成方向

            nextCenter = FindNextRoomCenter(lastCenter, nextLengh, nextWidth, pathLength, dir);

            if (curRoomCount == 0)
            {
                lastRoom = CreateRoom(Vector3.zero, 5, 5);

                lastCenter = lastRoom.Center;

                CreatePath(lastCenter, nextCenter, pathLength, dir, lastRoom);

                curRoomCount++;
            }


            while (!CheckSpaceEnough(nextCenter, nextLengh, nextWidth))
            {

                dir = Random.Range((int)RoomDir.Left, (int)RoomDir.Bottom + 1);


                nextCenter = FindNextRoomCenter(lastCenter, nextLengh, nextWidth, pathLength, dir);
            }
            Debug.Log("dir " + (RoomDir)dir);

            lastCenter = nextCenter;

            points.Add(nextCenter);

            lastRoom = CreateRoom(nextCenter, nextLengh, nextWidth);

            CreatePath(lastCenter, nextCenter, pathLength, dir, lastRoom);

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

    void CreatePath(Vector3 _lastCenter, Vector3 _nextCenter, int _pathLength, int _dir, RoomModel _lastRoom)
    {

        PathModel path = new PathModel();

        if (_dir == (int)RoomDir.Left)
        {

        }
        else if (_dir == (int)RoomDir.Right)
        {

        }
        else if (_dir == (int)RoomDir.Top)
        {

        }
        else if (_dir == (int)RoomDir.Bottom)
        {

        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_lastCenter">  上一个房间的中心点   </param>
    /// <param name="_nextLength">  下一个房间的长度    </param>
    /// <param name="_nextWidth">   下一个房间的宽度    </param>
    /// <returns></returns>
    Vector3 FindNextRoomCenter(Vector3 _lastCenter, int _nextLength, int _nextWidth, int _pathLength, int _dir)        //寻找下一个房间的原点
    {
        Vector3 nextCenter = _lastCenter;

        if (_dir == (int)RoomDir.Left)
        {
            nextCenter.x -= (_pathLength + _nextWidth * 0.5f);
        }
        else if (_dir == (int)RoomDir.Right)
        {
            nextCenter.x += (_pathLength - _nextWidth * 0.5f);
        }
        else if (_dir == (int)RoomDir.Top)
        {
            nextCenter.z += (_pathLength + _nextLength * 0.5f);
        }
        else if (_dir == (int)RoomDir.Bottom)
        {
            nextCenter.z -= (_pathLength - _nextLength * 0.5f);
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
