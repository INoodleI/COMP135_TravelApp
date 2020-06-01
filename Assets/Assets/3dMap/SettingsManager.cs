using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    public int homeLocation;
    public int tripDuration;
    public float maxBudget;

    private void Awake()
    {
        instance = this;
        homeLocation = PlayerPrefs.GetInt("homeLocation", -1);
        tripDuration = PlayerPrefs.GetInt("tripDuration", -1);
        maxBudget = PlayerPrefs.GetFloat("maxBudget", -1);
    }
}
