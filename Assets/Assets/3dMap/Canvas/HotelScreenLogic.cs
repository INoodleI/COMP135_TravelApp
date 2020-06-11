using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotelScreenLogic : MonoBehaviour
{
    public GameObject hotelSlotPrefab;
    public Transform slotsParent;
    public PlayerChoices pc;

    public List<HotelSlot> slots;

    public Fade nextButton;
    
    private void Start()
    {
    }

    public void LoadHotels(POIEntryData data)
    {
        Debug.Log("Loading In Hotels");
        pc.hotel = null;
        foreach (var g in slots)
        {
            Destroy(g.gameObject);
        }
        slots.Clear();
        foreach (var hotel in data.hotels)
        {
            Debug.Log("Loading Hotel: "+hotel.name);
            GameObject g = Instantiate(hotelSlotPrefab, slotsParent);
            HotelSlot slot = g.GetComponent<HotelSlot>();
            slots.Add(slot);
            slot.Init(hotel, this);
        }
    }
    public void HotelSelected(Hotel h, bool val)
    {
        if (pc.hotel == null && val)
        {
            pc.hotel = h;
            foreach (var entry in slots)
            {
                if(entry.hotel != h)
                    entry.toggleButton.SetFade(false);
            }
        }

        else if(pc.hotel == h && !val)
        {
            pc.hotel = null;
            foreach (var entry in slots)
            { 
                entry.toggleButton.SetFade(true);
            }
        }
    }

    private void Update()
    {
        if (pc.hotel != null && pc.getUsedBudget() <= pc.maxBudget)
        {
            nextButton.SetFade(true);
        }
        else
        {
            nextButton.SetFade(false);
        }
    }
}
