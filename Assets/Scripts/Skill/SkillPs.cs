using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPs : MonoBehaviour
{

    public float recycleTime = 5;
    private ParticleSystem[] ps;

    void Awake()
    {

        ps = GetComponentsInChildren<ParticleSystem>();
        if (ps.Length == 0)
        {
            ps = GetComponents<ParticleSystem>();
        }

    }

    public void ShowPs()
    {
        for (int i = 0; i < ps.Length; i++)
        {
            ps[i].Play();
        }
        StartCoroutine(Recycle());
    }

    public IEnumerator Recycle()
    {
        yield return new WaitForSeconds(recycleTime);
        ObjectPool.Instance.RecycleObject(this.gameObject.name, this.gameObject);
    }
}
