using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
