using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastCtrl : MonoBehaviour
{

    public int castId;
    public int damage = 10;
    public int speed = 5;
    public int liveTime = 3;

    private string castName;
    private bool fly = true;
    private List<int> buffIds = new List<int>();
    private List<Buff> buffs = new List<Buff>();

    private Vector3 flyDir;

    private float timer = 0;

    void Start()
    {
        Cast temp = CastConfig.GetCastById(this.castId);
        this.castId = temp.id;
        this.castName = temp.name;
        this.damage = temp.damage;
        this.speed = temp.speed;
        this.liveTime = temp.liveTime;

        if (temp.buffIds == null)
            return;
        this.buffIds = temp.buffIds;

        for (int i = 0; i < buffIds.Count; i++)
        {
            buffs.Add(BuffManager.GetBuffById(this.buffIds[i]));
        }
    }


    void Update()
    {

        if (fly)
        {
            timer += Time.deltaTime;

            this.transform.Translate(flyDir * this.speed * Time.deltaTime);

             CheckHit();

            if (timer > liveTime)
            {
                Recycle();
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.CompareTag("Enemy") == true)
    //    {
    //        other.transform.GetComponent<RoleBaseCtrl>().UpdateHp(-damage);
    //        EffectPerform.Instance.ShowDamageUI(damage, other.transform);
    //        BuffManager.AddBuffs(other.transform.GetComponent<RoleBaseCtrl>().RoleData, other.transform.GetComponent<ActorBuff>(), buffs);
    //        Recycle();
    //    }
    //}

    public void Fly(Vector3 flyDir)
    {
        this.fly = true;
        this.flyDir = flyDir;
        timer = 0;
    }

    Vector3 lastPos;

    Vector3 dir;

    public void CheckHit()
    {
        float length = (transform.position - lastPos).magnitude;
        RaycastHit hitinfo;

        lastPos = transform.position;
     
        if (Physics.Raycast(lastPos, flyDir, out hitinfo, speed * Time.deltaTime, LayerMask.GetMask("Enemy")))
        {
            if (this.buffs != null && hitinfo.transform.CompareTag("Enemy") == true)
            {
                hitinfo.transform.GetComponent<EnemyCtrl>().ReduceHealth(damage);
                EffectPerform.Instance.ShowDamageUI(damage, hitinfo.transform);
                BuffManager.AddBuffs(hitinfo.transform.GetComponent<RoleBaseCtrl>().RoleData, hitinfo.transform.GetComponent<ActorBuff>(), buffs);
                Recycle();
            }
        }
    }

    public void Recycle()
    {
        this.fly = false;

        ObjectPool.Instance.RecycleObject(this.gameObject.name, this.gameObject);
    }


}
