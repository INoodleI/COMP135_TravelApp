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

    public PlayerChoices pc;
    
    public CustomDropdown homeLocation;
    public TMP_InputField tripDuration;
    public TMP_InputField maxBudget;

    public TMP_InputField month;
    public TMP_InputField day;
    public TMP_InputField year;

    public List<Fade> reds;
    
    public bool correctDay;
    public DestinationScreenLogic dsl;

    // Start is called before the first frame update
    void Start()
    {
        //Lemme just add some code;
        int wasteofspace = 1;
        int another = 1;
        wasteofspace = another * 2;
        int fuckyou;
        //Set up Location Dropdown
        SetUpHomeLocationDropdown();
        homeLocation.selectedItemIndex = PlayerPrefs.GetInt("homeLocation", -1) + 1;
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
        
        //load saved start day
        int mm = PlayerPrefs.GetInt("tripStartMonth", -1);
        int dd = PlayerPrefs.GetInt("tripStartDay", -1);
        int yy = PlayerPrefs.GetInt("tripStartYear", -1);

        if (mm > 0 || mm <= 12)
            month.text = "" + mm;
        if (dd > 0 || dd <= 31)
            day.text = "" + dd;
        if (yy > 2019)
            year.text = "" + yy;

       // pc = PlayerChoices.instance;
        pc.home = PlayerPrefs.GetInt("homeLocation", -1);
        pc.maxBudget = budget;
        pc.tripDuration =  days;
        pc.tripStartDay[0] = mm;
        pc.tripStartDay[1] = dd;
        pc.tripStartDay[2] = yy;
        
        checkStartDay();
    }

    public void Initialize()
    {
        SetPOITarget(PlayerPrefs.GetInt("homeLocation", -1));
    }
    public void SetPOITarget(int index)
    {
        EarthPOI.instance.SetCurrentPOI(index);
    }

    public void SaveBudgetToPlayerPrefs(string text)
    {
        text = Regex.Replace(text, @"[^0-9]", "");
        int budget = -1;
        if(text != "")
            budget = Convert.ToInt32(text);
        
        PlayerPrefs.SetInt("maxBudget",budget);
        pc.maxBudget = budget;
    }
    public void SaveDurationToPlayerPrefs(string text)
    {
        text = Regex.Replace(text, @"[^0-9]", "");
        int days = -1;
        if(text != "")
            days = Convert.ToInt32(text);
        
        PlayerPrefs.SetInt("tripDuration",days);
        pc.tripDuration = days;
    }

    public void SaveLocationToPlayerPrefs(int newValue)
    {
        newValue -= 1;
        PlayerPrefs.SetInt("homeLocation",newValue);
        dsl.DropdownChanged(0);
        SetPOITarget(newValue);
        pc.home = newValue;
    }

    public void SetUpHomeLocationDropdown()
    {
        EarthPOI poiSystem = EarthPOI.instance;
        foreach (var entry in poiSystem.requestPOI())
        {
            homeLocation.CreateNewItem(entry.name, locationIcon);
        }
    }

    public void checkStartDay()
    {
    //    pc = PlayerChoices.instance;
        int mm, dd, yy;
        string mText = Regex.Replace(month.text, @"[^0-9]", "");
        month.text = mText;
        if(mText != "")
            mm = Convert.ToInt32(mText);
        else
            mm = -1;
        
        string dText = Regex.Replace(day.text, @"[^0-9]", "");
        day.text = dText;
        if(dText != "")
            dd = Convert.ToInt32(dText);
        else
            dd = -1;
        
        string yText = Regex.Replace(year.text, @"[^0-9]", "");
        year.text = yText;
        if(yText != "")
            yy = Convert.ToInt32(yText);
        else
            yy = -1;

//        Debug.Log(mm+"/"+dd+"/"+yy);
        
        pc.tripStartDay[0] = mm;
        pc.tripStartDay[1] = dd;
        pc.tripStartDay[2] = yy;
        
        PlayerPrefs.SetInt("tripStartMonth", mm);
        PlayerPrefs.SetInt("tripStartDay", dd);
        PlayerPrefs.SetInt("tripStartYear", yy);
        
        correctDay = true;
        bool fM = false, fD = false, fY = false;
        
        int dLow = 1, dHigh = 0;
        switch (mm)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                dHigh = 31;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                dHigh = 30;
                break;
            case 2:
                if (yy % 4 == 0)
                    dHigh = 29;
                else
                    dHigh = 28;
                break;
            default:
                correctDay = false;
                fM = true;
                break;
        }
       // Debug.Log(dLow+" < "+dd+" < "+dHigh);

        if (dd < dLow || dd > dHigh)
        {
            correctDay = false;
            fD = true;
        }

        if (yy < 2020)
        {
            correctDay = false;
            fY = true;
        }

        if (!correctDay)
        {
            reds[0].SetFade(fM);
            reds[1].SetFade(fD);
            reds[2].SetFade(fY);
         //   Debug.Log("Color Set To Red");
        }
        else
        {
            foreach (var VARIABLE in reds)
            {
                VARIABLE.SetFade(false);
            }
        }
    }
}
