using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoices : MonoBehaviour
{
    public static PlayerChoices instance;

    public int home;
    public int maxBudget;
    public int tripDuration;

    public int destination;
    public int planeCost;
    public string planeType;
    
    public List<Attractions> attractions;
    
    public void Awake()
    {
        instance = this;
    }
}
