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


    public void CreateRoomFlow(int _roomlength, int _roomWidth)   //初始化 第一个房间
    {
        CreateFirstRoom(_roomlength, _roomWidth);

        int targetRoomCount = 2;  //主房间总数，可随机

        int tryCount = 5000; //尝试次数

        int curRoomCount = 1;

        int count = 0;

        while (curRoomCount < targetRoomCount || count >= tryCount)
        {
            count++;
            CreateMainRoom(Rooms[Length - 1]);
            curRoomCount++;
        }
    }
    private void CreateFirstRoom(int _roomlength, int _roomWidth)
    {
        int x = Random.Range(1, this.width);
        int z = Random.Range(1, this.length);

        //判断房间下标是否越界

        if (x + _roomWidth > this.width || z + _roomlength > this.length)
        {
            x = x - _roomWidth;
            z = z - _roomlength;
        }


        Debug.Log("房间起始点x ： " + x + "房间起始点z: " + z);

        for (int i = x; i < x + _roomWidth; i++)
        {
            for (int j = z; j < z + _roomlength; j++)
            {
                this.coordinates[i, j] = (int)MapUnitType.Floor;

                Room tmpRoom = new Room(x, z, _roomlength, _roomWidth);

                this.rooms.Add(tmpRoom);
            }
        }

    }

    private void CreateMainRoom(Room _lastRoom)
    {
        int nextRoomLength = 8;   //可随机
        int nextRoomWidth = 8;
        int pathLength = 5;

        int dir = Random.Range(0, 3);

        while (_lastRoom.UsedDir.Contains(dir))
        {
            dir = Random.Range(0, 3);  //0 left 1 right 2 up 3 down
            break;
        }

        _lastRoom.UsedDir.Add(dir);
        
        bool isEnough = CheckAreaEnough(_lastRoom, dir, pathLength, nextRoomLength, nextRoomWidth);

        Debug.Log(isEnough);

        if (isEnough)
        {
            
        }
        else
        {
 
        }

    }

    private void CreateSubRoom(int _dir, int _nextLength, int _nextWidth, int _pathLength)   //创建子房间
    {

    }

    bool CheckAreaEnough(Room _lastRoom, int _dir, int _pathLength, int _nextLength, int _nextWidth)
    {
        switch (_dir)
        {
            case 0:
                {
                    for (int i = _lastRoom.StartX; i >= _lastRoom.StartX - _pathLength - _nextLength; i--)
                    {
                        for (int j = 0; j < _nextWidth; j++)
                        {
                            if (this.Coordinates[i, j] != (int)MapUnitType.Floor)
                            {
                                return false;
                            }
                        }
                    }
                }
                break;
            case 1:
                { }
                break;
            case 2:
                { }
                break;
            case 3:
                { }
                break;
            default:
                break;
        }
        return true;
    }

}

public class Room
{
    int startX;
    public int StartX { get { return startX; } }

    int startZ;

    public int StartZ { get { return startZ; } }

    int length;
    public int Length { get { return length; } }   //长

    int width;
    public int Width { get { return width; } } //宽

    private List<int> usedDir;  //使用过的方向
    public List<int> UsedDir { get { return usedDir; } }

    private int[,] doorPoint;  //门坐标点

    private int endX;

    private int endZ;

    public Room(int _sx, int _sz, int _length, int _width)
    {
        this.startX = _sx;
        this.startZ = _sz;
        this.length = _length;
        this.width = _width;

        this.endX = startX + width - 1;
        this.endZ = startZ + length - 1;

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

        map.CreateRoomFlow(10, 10);

        DrawMap();
    }


    void DrawMap()
    {
        Debug.Log("开始绘制地图！");
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
                // Debug.Log((map.Coordinates[i, j]));
                if (map.Coordinates[i, j] == (int)MapUnitType.Floor)
                {
                    GameObject go = Instantiate(floorPf);
                    go.transform.position = new Vector3(i, 0, j);
                }
                else if (map.Coordinates[i, j] == (int)MapUnitType.Wall)
                {

                }
            }
        }
    }
}
