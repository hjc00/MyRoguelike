using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathModel
{

    int length;  //道路的长度

    public int Length
    {
        get { return length; }
    }

    int width;  //道路的长度

    public int Width
    {
        get { return width; }
    }

    public PathModel()
    {
        this.length = 0;
        this.width = 0;
    }


    public PathModel(int _length, int _width)
    {
        this.length = _length;
        this.width = _width;
    }

}
