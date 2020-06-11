using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotelSlot : MonoBehaviour
{
    public HotelScreenLogic hsl;
    public Hotel hotel;
    public Fade toggleButton;
    public Image sprite;
    public TMP_Text name;
    public TMP_Text rating;
    public TMP_Text capacity;
    public TMP_Text breakfast;
    public TMP_Text cost;

    public void Init(Hotel h, HotelScreenLogic hhh)
    {
        hsl = hhh;
        hotel = h;
        sprite.sprite = h.image;
        name.text = h.name;
        rating.text = h.rating + " / 5";
        capacity.text = h.capacity + " people";
        cost.text = "" + h.cost;
        breakfast.text = (h.breakfast)?"Yes":"No" + " breakfast";
        toggleButton.SetFade(true);
    }

    public void ToggleToggled(bool val)
    {
        hsl.HotelSelected(hotel, val);
    }
}

[Serializable]
public class Hotel
{
    public string name;
    public Sprite image;
    public int rating;
    public int capacity;
    public bool breakfast;
    public int cost;
}