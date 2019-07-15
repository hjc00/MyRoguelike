using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastCtrl : MonoBehaviour
{
    public int damage = 10;  //default
    private Vector3 flyDir;
    public int speed = 8;
    private bool fly = true;
    private float timer = 0;
    public int liveTime = 5;
    // Use this for initialization
    void Start()
    {

    }


    void Update()
    {

        if (fly)
        {
            timer += Time.deltaTime;

            this.transform.Translate(this.flyDir * this.speed * Time.deltaTime);
            // Debug.Log(this.transform.position.y);

            //  CheckHit();

            if (timer > liveTime)
            {
                Destroy(gameObject);
            }
        }
    }


    public void Fly(Vector3 flydir)
    {
        this.fly = true;
        this.flyDir = flydir;
        // this.transform.rotation = Quaternion.Euler(flyDir);
        timer = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") == true)
        {
            other.transform.GetComponent<RoleBaseCtrl>().UpdateHp(-damage);
            EffectPerform.Instance.ShowDamageUI(damage, other.transform);
            Destroy(gameObject);
        }
    }
}
