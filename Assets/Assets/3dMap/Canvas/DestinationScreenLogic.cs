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
    public EarthPOI poi;
    public WindowManager windows;
    public AttractionScreenLogic asLogic;
    public HotelScreenLogic hsLogic;
    public CustomDropdown destination;
    public Sprite locationIcon;
    public PlayerChoices pc;
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
        foreach (var entry in poiSystem.requestPOI())
        {
            destination.CreateNewItem(entry.name, locationIcon);
        }
        destination.dropdownEvent.AddListener(DropdownChanged);
    }

    public void DropdownChanged(int value)
    {
        value -= 1;
        pc.destination = value;
        poi.SetCurrentPOI(value);
        pc.attractions.Clear();

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
            POIEntryData data = poi.requestPOI()[value].entryData;
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
                float angle = Quaternion.Angle(poi.requestPOI()[pc.home].tack.transform.rotation, poi.requestPOI()[value].tack.transform.rotation);
                economyCost = (int) (angle) * 3 + 200;
                businessCost = (int)(economyCost * 2.5f);
                economyText.text = "Economy Class:\n$" + economyCost;
                businessText.text = "Business Class:\n$" + businessCost;
                
                economyPlane.SetFade(true);
                businessPlane.SetFade(true);
                noPlaneButton.SetFade(false);
            }
            asLogic.LoadAttractions(data);
            hsLogic.LoadHotels(data);
        }
    }

    public void businessChoice()
    {
        pc.planeType = "Business Class";
        pc.planeCost = businessCost;
        windows.OpenPanel("HotelScreen");
    }
    public void economyChoice()
    {
        pc.planeType = "Economy Class";
        pc.planeCost = economyCost;
        windows.OpenPanel("HotelScreen");
    }
    public void noPlaneChoice()
    {
        pc.planeType = "No Plane Required!";
        pc.planeCost = 0;
        pc.hotel = null;
        windows.OpenPanel("AttractionsScreen");
    }
}
