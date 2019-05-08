using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public string icon { get; private set; }


    public int range { get; private set; }
    public int type { get; set; }
    public int cd { get; set; }
    public int effectRange { get; private set; }
    public int roleMoveDir { get; private set; }
    public int roleMoveDis { get; set; }
    public List<int> buffId { get; set; }
    public string anim { get; set; }
    public List<string> vfx { get; set; }


    public Skill(int id, string name, string desc, string icon, int range, int type, int cd,
        int effectRange, int roleMoveDir, int roleMoveDis, List<int> buffId, string anim, List<string> vfx)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = desc;
        this.icon = icon;
        this.range = range;
        this.type = type;
        this.cd = cd;
        this.effectRange = effectRange;
        this.roleMoveDir = roleMoveDir;
        this.roleMoveDis = roleMoveDis;
        this.buffId = buffId;
        this.anim = anim;
        this.vfx = vfx;
    }

    public void Use(Transform user)
    {
        if (this.roleMoveDir != 0)
        {
            MoveRole(user);
        }
        if (this.vfx != null)
        {
            ShowVfx(user.transform.position);
        }
        // Debug.Log("use type 1");
        CheckEnemy(user.position, this.range * 0.5f, this.range * 0.5f, this.range * 0.5f);
    }

    public void Use(Transform user, Vector3 pos)
    {
        if (this.roleMoveDir == 0)
        {
            MoveRole(user, pos);
        }
        if (this.vfx != null)
        {
            ShowVfx(pos);
        }
        // Debug.Log("use type 2");
        CheckEnemy(pos, this.effectRange * 0.5f, this.effectRange * 0.5f, this.effectRange * 0.5f);
    }

    private void ShowVfx(Vector3 pos)
    {
        for (int i = 0; i < this.vfx.Count; i++)
        {
            // Debug.Log(this.vfx[i]);
            GameObject tempVfx = ObjectPool.Instance.SpawnVfx(this.vfx[i]);
            tempVfx.GetComponent<SkillPs>().ShowPs();
            tempVfx.transform.position = pos;
        }
    }

    private void CheckEnemy(Vector3 pos, float halfExtentX, float halfExtentY, float halfExtentZ)
    {
        Collider[] cols;


        cols = Physics.OverlapBox(pos, new Vector3(halfExtentX, halfExtentY, halfExtentZ), Quaternion.identity, LayerMask.GetMask("Enemy"));

        if (cols.Length > 0)
            DoBuff(cols);


    }


    private void DoBuff(Collider[] cols)
    {
        if (this.buffId.Count == 0)
            return;

        List<Buff> buffs = new List<Buff>();

        for (int i = 0; i < buffId.Count; i++)
        {
            if (buffId[i] != 0)
                buffs.Add(BuffManager.GetBuffById(this.buffId[i]));
        }

        for (int i = 0; i < cols.Length; i++)
        {


            ActorBuff actorBuff = cols[i].GetComponent<ActorBuff>();
            BuffManager.AddBuffs(cols[i].GetComponent<RoleBaseCtrl>().RoleData, actorBuff, buffs);
        }
    }

    private void MoveRole(Transform user)
    {
        if (this.roleMoveDir == 1)
        {
            SkillPerform.Instance.Forward(user, 0.1f, this.roleMoveDis, this.roleMoveDis);   //fix duration 表配置
        }
        else if (this.roleMoveDir == 2)
        {
            SkillPerform.Instance.BeatBack(user, 0.1f, this.roleMoveDis);
        }
    }

    private void MoveRole(Transform user, Vector3 pos)
    {
        SkillPerform.Instance.Forward(user, pos, 0.1f, this.roleMoveDis);   //fix duration 表配置
    }
}
