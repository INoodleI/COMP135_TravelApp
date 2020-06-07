using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BudgetBar : MonoBehaviour
{
    public RectTransform redBar;
    public RectTransform greenBar;
    public TMP_Text budget;
    private Vector2 redBarRange;
    private PlayerChoices pc;

    private void Start()
    {
        pc = PlayerChoices.instance;
        redBarRange = new Vector2(redBar.offsetMax.x, greenBar.offsetMax.x);
    }

    public void Update()
    {
        int usedBudget = pc.planeCost;
        foreach (var attraction in pc.attractions)
        {
            usedBudget += attraction.cost;
        }
        Debug.Log(usedBudget);
        budget.text = "Budget: $" + usedBudget + " / $" + pc.maxBudget;
        redBar.offsetMax = new Vector2(ReMap(usedBudget, new Vector2(0,pc.maxBudget), redBarRange), 0);
    }

    public float ReMap(float val, Vector2 oldRange, Vector2 newRange)
    {
        float percent = (val - oldRange.x) / (oldRange.y - oldRange.x);
        return (newRange.y - newRange.x) * percent + newRange.x;
    }
}
