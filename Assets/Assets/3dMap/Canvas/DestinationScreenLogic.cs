using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class DestinationScreenLogic : MonoBehaviour
{
    public AttractionScreenLogic asLogic;
    public CustomDropdown destination;
    public Sprite locationIcon;
    private PlayerChoices pc;
    [FormerlySerializedAs("nextButton")] public Fade noPlaneButton;
    public Fade economyPlane;
    public TMP_Text economyText;
    public Fade businessPlane;
    public TMP_Text businessText;
    private int economyCost;
    private int businessCost;

    public Image displayImage;
    public TMP_Text description;

    public void Start()
    {
        SetUpLocationDropdown();
        pc = PlayerChoices.instance;
        noPlaneButton.SetFade(false);
        economyPlane.SetFade(false);
        businessPlane.SetFade(false);
        
        displayImage.sprite = null;
        displayImage.enabled = false;
        description.text = "";
    }

    public void SetUpLocationDropdown()
    {
        EarthPOI poiSystem = EarthPOI.instance;
        foreach (var entry in poiSystem.POI)
        {
            destination.CreateNewItem(entry.name, locationIcon);
        }
        destination.dropdownEvent.AddListener(DropdownChanged);
    }

    public void DropdownChanged(int value)
    {
        value -= 1;
        pc.destination = value;
        EarthPOI.instance.SetCurrentPOI(value);

        if (value == -1)
        {
            displayImage.sprite = null;
            displayImage.enabled = false;
            description.text = "";
            noPlaneButton.SetFade(false);
            economyPlane.SetFade(false);
            businessPlane.SetFade(false);
        }
        else
        {
            displayImage.enabled = true;
            POIEntryData data = EarthPOI.instance.POI[value].entryData;
            displayImage.sprite = data.coverPhoto;
            description.text = data.coverDescription.Replace("\\n","\n");
            if (value == pc.home)
            {
                economyPlane.SetFade(false);
                businessPlane.SetFade(false);
                noPlaneButton.SetFade(true);
            }
            else
            {
                float angle = Quaternion.Angle(EarthPOI.instance.POI[pc.home].tack.transform.rotation, EarthPOI.instance.POI[value].tack.transform.rotation);
                economyCost = (int) (angle) * 3 + 200;
                businessCost = (int)(economyCost * 2.5f);
                economyText.text = "Economy Class:\n$" + economyCost;
                businessText.text = "Business Class:\n$" + businessCost;
                
                economyPlane.SetFade(true);
                businessPlane.SetFade(true);
                noPlaneButton.SetFade(false);
            }
            asLogic.LoadAttractions(data);
        }
    }

    public void businessChoice()
    {
        pc.planeType = "Business Class";
        pc.planeCost = businessCost;
    }
    public void economyChoice()
    {
        pc.planeType = "Economy Class";
        pc.planeCost = economyCost;
    }
    public void noPlaneChoice()
    {
        pc.planeType = "No Plane Required!";
        pc.planeCost = 0;
    }
}
