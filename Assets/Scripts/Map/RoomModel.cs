using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomDir
{
    Left = 0,
    Right,
    Top,
    Bottom,
}

public class RoomModel
{

    int length;
    public int Length { get { return length; } }

    int width;
    public int Width { get { return width; } }

    Vector3 center;
    public Vector3 Center { get { return center; } }

    Vector3 left;
    public Vector3 Left { get { return left; } }

    Vector3 right;
    public Vector3 Right { get { return right; } }

    Vector3 top;
    public Vector3 Top { get { return top; } }

    Vector3 bottom;
    public Vector3 Bottom { get { return bottom; } }

    public RoomModel(Vector3 _center, int _length, int _width)
    {
        this.center = _center;
        this.length = _length;
        this.width = _width;

        this.left = new Vector3(this.center.x - this.width * 0.5f, 0, this.center.y);

        this.right = new Vector3(this.center.x + this.width * 0.5f, 0, this.center.y);

        this.top = new Vector3(this.center.x, 0, this.center.y + this.length * 0.5f);

        this.bottom = new Vector3(this.center.x, 0, this.center.y - this.length * 0.5f);
    }

}
