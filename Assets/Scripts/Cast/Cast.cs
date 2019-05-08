using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class Cast
{
    public int id { get; set; }
    public string name { get; set; }
    public int damage { get; set; }
    public int speed { get; set; }
    public int liveTime { get; set; }
    public List<int> buffIds { get; set; }

}
