using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public WindowManager wManager;
    public PlayerChoices pc;
    public AttractionScreenLogic asl;
    public HotelScreenLogic hsl;
    public Fade self;

    private void Start()
    {
    }

    private void Update()
    {
        int index = wManager.currentWindowIndex;
        if (index < 3 || index > 6)
            self.SetFade(false);
        else
            self.SetFade(true);
    }

    public void OnPress()
    {
        int index = wManager.currentWindowIndex;
        Debug.Log("Back Button Pressed: "+index);
        switch (index)
        {
            case 3:                //Destination Screen
                wManager.OpenPanel("SettingsScreen");
                break;
            case 4:                //Attractions Screen
                if (pc.home == pc.destination)
                {
                    wManager.OpenPanel("DestinationScreen");
                    pc.planeCost = 0;
                    pc.planeType = "";
                }
                else
                {
                    wManager.OpenPanel("HotelScreen");
                    pc.hotel = null;
                    hsl.LoadHotels(EarthPOI.instance.requestPOI()[pc.destination].entryData);
                }
                pc.attractions.Clear();
                asl.LoadAttractions(EarthPOI.instance.requestPOI()[pc.destination].entryData);
                break;
            case 5:                //Hotel Screen
                Debug.Log("Trying To return from HotelScreen");
                wManager.OpenPanel("DestinationScreen");
                hsl.LoadHotels(EarthPOI.instance.requestPOI()[pc.destination].entryData);
                pc.planeCost = 0;
                pc.planeType = "";
                break;
            case 6:                //Final Screen
                wManager.OpenPanel("AttractionsScreen");
                break;
            default:
                Debug.Log("Random Back Button Pressed: wIndex "+index);
                break;
        }
    }
}
