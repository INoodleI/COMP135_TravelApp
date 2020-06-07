using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TackControl : MonoBehaviour
{
    [FormerlySerializedAs("obj")] public Transform pivot;
    public Transform tack;
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
        pivot.rotation = Quaternion.LookRotation(pivot.position - earthCam.position, earthCam.up);
    }

    public void SetFocus(bool t)
    {
        nametag.SetFade(t);
    }
}
