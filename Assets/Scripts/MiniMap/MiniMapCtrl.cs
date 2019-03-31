using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCtrl : MonoBehaviour
{

    public GameObject mapBg;
    public GameObject roleTexture;
    public GameObject roomTexture;
    public GameObject pathTexture;
    public GameObject plane;

    public float drawInterval = 0.1f;
    private float timer = 0;

    // Use this for initialization
    void Start()
    {
        drawMinMap();
    }

    public void drawMinMap()
    {
        for (int i = 0; i < MapGenerator.Instance.RoomCenterPoints.Count; i++)
        {
            if (i == 0)
            {

                roomTexture.transform.localPosition = new Vector3(MapGenerator.Instance.RoomCenterPoints[i].x * 2, MapGenerator.Instance.RoomCenterPoints[i].y * 2);
            }
            else
            {
                GameObject newRoomTexture = Instantiate(roomTexture);
                newRoomTexture.transform.SetParent(this.transform);
                newRoomTexture.transform.SetSiblingIndex(0);
                newRoomTexture.transform.localPosition = new Vector3(MapGenerator.Instance.RoomCenterPoints[i].x * 2, MapGenerator.Instance.RoomCenterPoints[i].y * 2);
            }
        }

        //for (int i = 0; i < MapGenerator.Instance.mapLenghth; i++)
        //{
        //    for (int j = 0; j < MapGenerator.Instance.mapLenghth; j++)
        //    {
        //        if (MapGenerator.Instance.Map[i, j] != -1)
        //        {
        //            GameObject newRoomTexture = Instantiate(roomTexture);
        //            newRoomTexture.transform.SetParent(this.transform);
        //            newRoomTexture.transform.SetSiblingIndex(0);
        //            newRoomTexture.transform.localPosition = new Vector2(i * 2, j * 2);
        //        }
        //    }

        //}
        DrawRole();
    }

    private void DrawRole()
    {
        Vector3 dir = NpcManager.Instance.Player.transform.position - plane.transform.position;

        float mx = NpcManager.Instance.Player.transform.position.x / 500;

        float my = NpcManager.Instance.Player.transform.position.z / 500;

        this.roleTexture.GetComponent<RectTransform>().localPosition = new Vector2(200f * mx, 200f * my);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= drawInterval)
        {
            timer = 0;
            DrawRole();
        }
    }
}
