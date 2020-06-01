using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreenNextButtonLogic : MonoBehaviour
{
    public Fade button;
    private void Update()
    {
        if (PlayerPrefs.GetInt("homeLocation", 0) == 0 || PlayerPrefs.GetInt("tripDuration", -1) == -1 ||
            PlayerPrefs.GetInt("maxBudget", -1) == -1)
        {
            button.SetFade(false);
        }
        else
        {
            button.SetFade(true);
        }
    }
}
