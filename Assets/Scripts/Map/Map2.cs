using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Map
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

    List<Room> rooms;
    public List<Room> Rooms { get { return rooms; } }

    public void AddRoom(Room _room)
    {
        rooms.Add(_room);
    }

    public Map(int _length, int _width, int _roomCount)
    {
        this.length = _length;
        this.width = _width;
        this.coordinates = new int[_length, _width];
        this.roomCount = _roomCount;
        this.rooms = new List<Room>();
        InitCoordinate();
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

    public void CreateRoom(int _roomlength, int _roomWidth)   //初始化 第一个房间
    {
        int x = Random.Range(1, this.width);
        int z = Random.Range(1, this.length);

        Debug.Log("房间起始点x ： " + x + "房间起始点z: " + z);

        //判断房间下标是否越界

        if (x + _roomWidth > this.width || z + _roomlength > this.length)
        {
            x = x - _roomWidth;
            z = z - _roomlength;
        }

        for (int i = x; i < _roomWidth; i++)
        {
            for (int j = z; j < _roomlength; j++)
            {
                this.coordinates[i, j] = 1;
                Room tmpRoom = new Room(x, z);
                this.rooms.Add(tmpRoom);
            }
        }


    }
}

public class Room
{
    int startX;
    public int StartX { get { return startX; } }

    int startZ;

    public int StartZ { get { return startZ; } }

    private List<int> usedDir;

    public Room(int _sx, int _sz)
    {
        this.startX = _sx;
        this.startZ = _sz;
        this.usedDir = new List<int>();
    }

    public void AddDir(int _dir)
    {
        this.usedDir.Add(_dir);
    }
}

enum MapUnitType
{
    Floor = 1,
    Wall,
}

public class Map2 : MonoBehaviour
{
    Map map;

    public GameObject floorPf;

    void Awake()
    {
        map = new Map(100, 100, 10);

        map.CreateRoom(10, 10);

        DrawMap();
    }


    void DrawMap()
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
                if (map.Coordinates[i, j] == (int)MapUnitType.Floor)
                {
                    GameObject go = Instantiate(floorPf);
                    go.transform.position = new Vector3(i, 0, j);
                    Debug.Log(1);
                }
                else if (map.Coordinates[i, j] == (int)MapUnitType.Wall)
                {

                }
            }
        }
    }
}
