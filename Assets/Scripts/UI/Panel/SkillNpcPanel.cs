﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillNpcPanel : BasePanel
{


    private int[] randomSkillIds = new int[3];

    public SkillCellUI[] skillCellUI;
    public GameObject buyPanel;
    public GameObject CellTemplate;

    public int selectSkillId { get; set; }

    public override void Awake()
    {
        skillCellUI = new SkillCellUI[3];
        randomSkillIds[0] = Random.Range(0, 3) + 1000;
        randomSkillIds[1] = Random.Range(0, 4) + 2000;
        randomSkillIds[2] = Random.Range(0, 4) + 2000;
        //randomSkillIds[0] = 2000;
        //randomSkillIds[1] = 2003;
        //randomSkillIds[2] = 2004;
        CerateCell();
        SetUpCell();
    }

    private void CerateCell()
    {
        skillCellUI[0] = this.CellTemplate.GetComponent<SkillCellUI>();
        //  Debug.Log(skillCellUI.Length);
        for (int i = 1; i < skillCellUI.Length; i++)
        {
            GameObject tempCell = Instantiate(this.CellTemplate);
            tempCell.transform.SetParent(this.CellTemplate.transform.parent);
            skillCellUI[i] = tempCell.GetComponent<SkillCellUI>();
        }
    }
    private void SetUpCell()
    {

        for (int i = 0; i < randomSkillIds.Length; i++)
        {
            skillCellUI[i].SetData(randomSkillIds[i], this);
        }
    }

    public void ShowBuy()
    {
        buyPanel.SetActive(true);
        buyPanel.transform.DOScale(1, 0.2f);
    }

    public void CloseBuy()
    {

        buyPanel.transform.DOScale(0, 0.2f).OnComplete(() =>
        {
            buyPanel.SetActive(false);
        });
    }

    public void ClickBuy()
    {
        this.CloseBuy();

        UIManager.Instance.ClosePanel(GameDefine.skillNpcPanel);
        EventCenter.Broadcast(EventType.DestroySkillNpc);
        EventCenter.Broadcast<int>(EventType.OnLearnSkill, this.selectSkillId);
    }

}
