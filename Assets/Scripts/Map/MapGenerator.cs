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
    }
};


public class MapGenerator : MonoBehaviour
{
    MapModel map;

    public GameObject cubePf;

    void Awake()
    {
        map = new MapModel(50, 50, 7);
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
        RoomModel firstRoom = CreateRoom(Vector3.zero, 10, 10);  //在原点创建第一个房间

        int curRoomCount = 1;

        Vector3 tempCenter = firstRoom.Center;

        while (curRoomCount < map.RoomCount)
        {

            FindNextRoomCenter(firstRoom.Center);

            curRoomCount++;
        }
    }
    RoomModel CreateRoom(Vector3 _center, int _long, int _width)
    {
        RoomModel room = new RoomModel(_center, _long, _width);

        int startX = (int)(room.Center.x - room.Width * 0.5f);  //循环开始的索引

        int startY = (int)(room.Center.y - room.Length * 0.5f);  //

        for (int i = startX; i < room.Width - 1; i++)
        {
            for (int j = startY; j < room.Length - 1; j++)
            {
                GameObject cubeGo = Instantiate(cubePf);

                cubeGo.transform.position = new Vector3(i, 0, j);
            }
        }
        return room;
    }

    Vector3 FindNextRoomCenter(Vector3 _lastCenter)        //寻找下一个房间的原点
    {
        Vector3 retCenter = Vector3.zero;

        int tmpDir = Random.Range();

        return retCenter;
    }

    bool CheckSpaceEnough()
    {
        return false;
    }

}
