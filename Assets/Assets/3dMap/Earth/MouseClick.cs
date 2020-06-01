using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour
{
    public EarthPOI poi;

    public int index = -1;
    // Update is called once per frame
    void Update()
    {
        poi.SetCurrentPOI(index);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index++;
            if (index == poi.POI.Count)
                index = -1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index--;
            if (index == -2)
                index = poi.POI.Count-1;
        }
    }
}
