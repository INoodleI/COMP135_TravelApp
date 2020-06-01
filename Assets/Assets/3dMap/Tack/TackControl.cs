using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TackControl : MonoBehaviour
{
    public Transform obj;
    private Transform earthCam;
    public Fade nametag;
    public TMP_Text text;

    private void Start()
    {
        earthCam = GameObject.FindWithTag("EarthCam").transform;
        SetFocus(false);
    }

    private void Update()
    {
        obj.rotation = Quaternion.LookRotation(obj.position - earthCam.position, earthCam.up);
    }

    public void SetFocus(bool t)
    {
        nametag.SetFade(t);
    }
}
