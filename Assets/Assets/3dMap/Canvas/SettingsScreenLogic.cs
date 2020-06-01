using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class SettingsScreenLogic : MonoBehaviour
{
    public Sprite locationIcon;
    
    public CustomDropdown homeLocation;
    public TMP_InputField tripDuration;
    public TMP_InputField maxBudget;

    // Start is called before the first frame update
    void Start()
    {
        //Set up Location Dropdown
        SetUpHomeLocationDropdown();
        homeLocation.selectedItemIndex = PlayerPrefs.GetInt("homeLocation", 0);
        homeLocation.dropdownEvent.AddListener(SaveLocationToPlayerPrefs);
        
        //Load saved Trip Duration
        int days = PlayerPrefs.GetInt("tripDuration", -1);
        if (days != -1)
            tripDuration.text = days.ToString();
        tripDuration.onValueChanged.AddListener(SaveDurationToPlayerPrefs);
        
        //Load saved Max Budget
        int budget = PlayerPrefs.GetInt("maxBudget", -1);
        if (budget != -1)
            maxBudget.text = budget.ToString();
        maxBudget.onValueChanged.AddListener(SaveBudgetToPlayerPrefs);
    }

    public void Initialize()
    {
        SetPOITarget(PlayerPrefs.GetInt("homeLocation", 0));
    }
    public void SetPOITarget(int index)
    {
        EarthPOI.instance.SetCurrentPOI(index-1);
    }

    public void SaveBudgetToPlayerPrefs(string text)
    {
        text = Regex.Replace(text, @"[^0-9]", "");
        int budget = -1;
        if(text != "")
            budget = Convert.ToInt32(text);
        
        PlayerPrefs.SetInt("maxBudget",budget);
    }
    public void SaveDurationToPlayerPrefs(string text)
    {
        text = Regex.Replace(text, @"[^0-9]", "");
        int days = -1;
        if(text != "")
            days = Convert.ToInt32(text);
        
        PlayerPrefs.SetInt("tripDuration",days);
    }

    public void SaveLocationToPlayerPrefs(int newValue)
    {
        PlayerPrefs.SetInt("homeLocation",newValue);
        SetPOITarget(newValue);
    }

    public void SetUpHomeLocationDropdown()
    {
        EarthPOI poiSystem = EarthPOI.instance;
        foreach (var entry in poiSystem.POI)
        {
            homeLocation.CreateNewItem(entry.name, locationIcon);
        }
    }
}
