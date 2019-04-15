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

    public int effectRange { get; private set; }
    public int roleMoveDir { get; private set; }
    public int roleMoveDis { get; set; }
    public int damage { get; set; }
    public int lowerSpeed { get; set; }
    public string anim { get; set; }
    public List<string> vfx { get; set; }


    public Skill(int id, string name, string desc, string icon, int range, int type,
        int effectRange, int roleMoveDir, int roleMoveDis, int damage, int lowerSpeed, string anim, List<string> vfx)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = desc;
        this.icon = icon;
        this.range = range;
        this.type = type;
        this.effectRange = effectRange;
        this.roleMoveDir = roleMoveDir;
        this.roleMoveDis = roleMoveDis;
        this.damage = damage;
        this.lowerSpeed = lowerSpeed;
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

        }

        DoDamage();

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
    }

    private void ShowVfx(Vector3 pos)
    {
        for (int i = 0; i < this.vfx.Count; i++)
        {
            Debug.Log(this.vfx[i]);
            GameObject tempVfx = ObjectPool.Instance.SpawnObj(this.vfx[i]);
            tempVfx.GetComponent<SkillPs>().ShowPs();
            tempVfx.transform.position = pos;
        }
    }

    private void DoDamage()
    {

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
