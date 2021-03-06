﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EarthPOI : MonoBehaviour
{
    public static EarthPOI instance;
    [SerializeField]
    private List<POIEntry> POI = new List<POIEntry>();
    public GameObject thumbTackPrefab;

    public CamToPOI camToPoi;
    public int currentPOI;
    
    public float dist;

    private bool initialized = false;

    private void Awake()
    {
        instance = this;
        Alphabetize();
        foreach (var entry in POI)
        {
            SpawnInfoMarker(entry);
        }

        currentPOI = -1;
    }

    private void Start()
    {
        SetCurrentPOI(-1);
    }


    public List<POIEntry> requestPOI()
    {
        if (!initialized)
        {
            Alphabetize();
            foreach (var entry in POI)
            {
                SpawnInfoMarker(entry);
            }
        }

        return POI;
    }

    private void Alphabetize()
    {
        List<POIEntry> ordered = new List<POIEntry>();
        foreach (var entry in POI)
        {
            int i = 0;
            foreach (var check in ordered)
            {
                if (check.name.CompareTo(entry.name) > 0)
                    break;
                i++;
            }
            ordered.Insert(i,entry);
        }
        POI = ordered;
    }

    private void SpawnInfoMarker(POIEntry e)
    {
        if (e.tack == null)
        {
            e.tack = Instantiate(thumbTackPrefab, transform);
            e.tackControl = e.tack.GetComponent<TackControl>();
            e.tackControl.text.text = e.name;
            Debug.Log(e.name+" : "+e.tackControl.text.text);
            e.tackControl.SetFocus(false);
        }

        e.tack.transform.localEulerAngles = new Vector3(e.y, e.x, 0);
    }

    private void OnDrawGizmos()
    {
        foreach (var entry in POI)
        {
            SpawnInfoMarker(entry);
        }
    }

    public void SetCurrentPOI(int index)
    {
        if (index == currentPOI)
            return;
        
        //Make Nametags show up
        if(currentPOI < POI.Count && currentPOI >= 0)
            POI[currentPOI].tackControl.SetFocus(false);
        if(index < POI.Count && index >= 0)
            POI[index].tackControl.SetFocus(true);
        currentPOI = index;
        
        //Stop camera spin with planet, unfocus
        if (index >= POI.Count || index < 0)
        {
            camToPoi.targetDist = -1f;
            camToPoi.SpinWithParent(false);
            return;
        }

        //Make camera focus on pin
        camToPoi.SpinWithParent(true);
        camToPoi.SetTargetRot(Quaternion.Euler(new Vector3(POI[currentPOI].y, POI[currentPOI].x, 0)));
        camToPoi.SetTargetDist(-0.8f);
    }
}