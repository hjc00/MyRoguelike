using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{

    public Dictionary<string, List<GameObject>> pool;

    public Dictionary<string, GameObject> prefabs;

    private static ObjectPool instance;

    public static ObjectPool Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {

        pool = new Dictionary<string, List<GameObject>>();
        prefabs = new Dictionary<string, GameObject>();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreatePool(string poolName, string path, int cnt)
    {
        if (pool.ContainsKey(poolName))
        {
            Debug.Log("已存在该对象池");
            return;
        }

        List<GameObject> newPool = new List<GameObject>();

        GameObject prefab = Resources.Load<GameObject>(path);

        prefabs.Add(poolName, prefab);

        for (int i = 0; i < cnt; i++)
        {
            GameObject temp = Instantiate(prefab);
            temp.transform.SetParent(this.transform);
            temp.name = poolName;
            temp.SetActive(false);
            newPool.Add(temp);
        }
    }

    public GameObject SpawnObj(string poolName)
    {
        if (!pool.ContainsKey(poolName))
        {
            Debug.Log("对象池不存在！请先创建");
            return null;
        }
        List<GameObject> tempPool = pool[poolName];

        GameObject res = null;
        if (tempPool.Count > 0)
        {
            res = tempPool[tempPool.Count - 1];
            res.SetActive(true);
            tempPool.Remove(res);
        }
        else
        {
            res = Instantiate(prefabs[poolName]);
            res.name = poolName;
            res.SetActive(true);
        }
        return res;
    }

    public void RecycleObject(string poolName, GameObject go)
    {
        if (!pool.ContainsKey(poolName))
        {
            Debug.Log("对象池不存在！请先创建");
            return;
        }
        go.SetActive(false);
        pool[poolName].Add(go);
    }
}
