using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttractionSlot : MonoBehaviour
{
    public AttractionScreenLogic asl;
    public Attractions attraction;
    public Image sprite;
    public TMP_Text name;
    public TMP_Text rating;
    public TMP_Text timeCommitment;
    public TMP_Text desc;
    public TMP_Text cost;
    public GameObject toggle;

    public void Init(Attractions a, AttractionScreenLogic aaaaa, bool active)
    {
        asl = aaaaa;
        attraction = a;
        sprite.sprite = a.sprite;
        name.text = a.name;
        rating.text = a.rating + " / 5";
        timeCommitment.text = a.time_hours+" hours";
        cost.text = ""+a.cost;
        desc.text = a.description;
        toggle.SetActive(active);
    }

    public void ToggleToggled(bool val)
    {
        asl.AttractionToggled(attraction, val);
    }
}
