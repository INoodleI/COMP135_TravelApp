using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoices : MonoBehaviour
{
    //public static PlayerChoices instance;

    public int home;
    public int maxBudget;
    public int tripDuration;
    public int [] tripStartDay;

    public int destination;
    public int planeCost;
    public string planeType;
    
    public List<Attractions> attractions;
    public Hotel hotel;
    
    public void Awake()
    {
    //    instance = this;
        tripStartDay = new int[3];
    }

    public int getUsedBudget()
    {
        int usedBudget = planeCost;
        if (hotel != null)
            usedBudget += hotel.cost;
        foreach (var attraction in attractions)
        {
            usedBudget += attraction.cost;
        }

        return usedBudget;
    }
}
