using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

[Serializable]
public class LevelInfo
{
    public int id;
    public int width;
    public int length;
    public int roomMinWidth;
    public int roomMaxWidth;
    public int pathMinLength;
    public int pathMaxLength;
    public int maxRoomCnt;
    public int mapCellMul;

    public string wallPf;
    public string pathPf;
    public string floorPf;
    public string bossPf;
    public string enemyPf;
}
