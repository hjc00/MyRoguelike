using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapUnit
{
    None = 0,
    Floor = 1,
    Wall,
}

public class Room
{
    int startX;

    public int StartX { get { return startX; } }

    int startZ;

    public int StartZ { get { return startZ; } }

    int length;

    public int Length { get { return length; } }

    int width;

    public int Width { get { return width; } }


    public Room()
    {
        startX = 0;
        startZ = 0;
    }

    public Room(int _sx, int _sz, int _l, int _w)
    {
        startX = _sx;
        startZ = _sz;
        length = _l;
        width = _w;
    }
}

public class Map : MonoBehaviour
{
    public GameObject floorPf;

    int[,] map;

    List<Room> rooms = new List<Room>();

    private void Start()
    {
        InitMap(100, 100);
        CreateRoom(100, 100);
        CreateRoom(100, 100);
        CreateRoom(100, 100);
        CreateRoom(100, 100);
        DrawMap();
    }

    void InitMap(int _length, int _width)
    {
        map = new int[_length, _width];

        for (int i = 0; i < _length; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                map[i, j] = (int)MapUnit.None;
            }
        }
    }


    void CreateRoom(int _length, int _width)
    {
        int x = Random.Range(1, 99);
        int z = Random.Range(1, 99);

        int roomLength = 10;
        int roomWidth = 10;

        if (x >= _width)
        {
            x -= roomWidth;
        }
        if (z >= _length)
        {
            z -= roomLength;
        }

        Room tmpRoom = new Room(x, z, roomLength, roomWidth);

        bool isOverLap = CheckOverlep(tmpRoom);

        if (isOverLap)
        {
            Debug.Log("生成的房间有重叠！");
            return;
        }

        map[x, z] = (int)MapUnit.Floor;

        Debug.Log("x: " + x + " z: " + z + " map[x,z]: " + map[x, z]);
    }

    bool CheckOverlep(Room tmp)
    {
        for (int i = 0; i < rooms.Count; i++)
        {

        }
        return false;
    }

    void DrawMap()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (map[i, j] == 1)
                {
                    GameObject floor = Instantiate(floorPf);
                    floorPf.transform.position = new Vector3(i, 0, j);
                }
            }
        }
    }

}
