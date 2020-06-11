using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class POIEntryData
{
    public string name;
    [Header("Destination Screen")]
    public Sprite coverPhoto;
    public string coverDescription;
    [Range(0,5)]
    public int rating;
    public Vector2 budgetRange;
    public List<Hotel> hotels;
    public List<Attractions> attractions;
}
