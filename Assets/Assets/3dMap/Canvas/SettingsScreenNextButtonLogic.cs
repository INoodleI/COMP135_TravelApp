using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreenNextButtonLogic : MonoBehaviour
{
    public Fade button;
    public SettingsScreenLogic ss;
    private void Update()
    {
        if (PlayerPrefs.GetInt("homeLocation", -1) == -1 || PlayerPrefs.GetInt("tripDuration", -1) == -1 ||
            PlayerPrefs.GetInt("maxBudget", -1) == -1 || !ss.correctDay)
        {
            button.SetFade(false);
        }
        else
        {
            button.SetFade(true);
        }
    }
}
