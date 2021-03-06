﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine; 

public class AttractionScreenLogic : MonoBehaviour
{
    public GameObject attractionSlotPrefab;
    public Transform slotsParent;
    
    public TMP_Text title;
    private PlayerChoices pc;

    public List<AttractionSlot> slots;
    public BudgetBar bBar;

    public Fade nextButton;
    public Fade overBudgetText;
    public void Start()
    {
        pc = GameObject.FindGameObjectWithTag("PC").GetComponent<PlayerChoices>();
    }

    public void LoadAttractions(POIEntryData data)
    {
        title.text = data.name;
        pc.attractions.Clear();
        
        List<Attractions> attractions = data.attractions;
        foreach (var g in slots)
        {
            Destroy(g.gameObject);
        }
        slots.Clear();
        foreach (var a in attractions)
        {
            GameObject g = Instantiate(attractionSlotPrefab, slotsParent);
            AttractionSlot slot = g.GetComponent<AttractionSlot>();
            slots.Add(slot);
            slot.Init(a, this, true);
        }
    }

    public void AttractionToggled(Attractions a, bool val)
    {
        if (pc.attractions.Contains(a) == val)
            return;

        if (val)
        {
            pc.attractions.Add(a);
        }
        else
        {
            pc.attractions.Remove(a);
        }
    }

    private void LateUpdate()
    {
        if (bBar.usedBudget > pc.maxBudget)
        {
            overBudgetText.SetFade(true);
            nextButton.SetFade(false);
        }
        else
        {
            overBudgetText.SetFade(false);
            nextButton.SetFade(true);
        }
    }
}
