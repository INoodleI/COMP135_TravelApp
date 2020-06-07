using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EarthPOI : MonoBehaviour
{
    public static EarthPOI instance;
    public List<POIEntry> POI = new List<POIEntry>();
    public GameObject thumbTackPrefab;

    public CamToPOI camToPoi;
    public int currentPOI;
    
    public float dist;

    private void Awake()
    {
        instance = this;
        Alphabetize();
        foreach (var entry in POI)
        {
            SpawnInfoMarker(entry);
        }

        currentPOI = -9;
    }

    private void Start()
    {
        SetCurrentPOI(-1);
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

[Serializable]
public class POIEntry
{
    public string name;
    [Range(-180,180)]
    public float x;
    [Range (-89,89)]
    public float y;
    public GameObject tack = null;
    public TackControl tackControl;
    public POIEntryData entryData;
}

[CreateAssetMenu(menuName = "Earth POI System/Entry data")]
public class POIEntryData : ScriptableObject
{
    [Header("Destination Screen")]
    public Sprite coverPhoto;
    public string coverDescription;
    [Range(0,5)]
    public int rating;
    public Vector2 budgetRange;
    public List<Attractions> attractions;
}

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