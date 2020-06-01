using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

[RequireComponent(typeof(CustomDropdown))]
public class FadeOnDropdown : MonoBehaviour
{
    public List<Fade> objects;
    private CustomDropdown customDropdown;

    private void Start()
    {
        customDropdown = GetComponent<CustomDropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var f in objects)
        {
            f.SetFade(!customDropdown.isOn);
        }
    }
}
