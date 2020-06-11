using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class Attractions
{
    public String name;
    public Sprite sprite;
    public string description;
    [Range(0,5)]
    public int rating;
    public int cost;
    public int time_hours;
}