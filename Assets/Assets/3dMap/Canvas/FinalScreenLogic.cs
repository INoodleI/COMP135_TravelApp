using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;

public class FinalScreenLogic : MonoBehaviour
{
    public PlayerChoices pc;
    public GameObject attractionPrefab;
    public HotelSlot hs;

    public TMP_Text destination;
    public TMP_Text days;
    public TMP_Text plane;
    public TMP_Text maxBudget;

    public Transform slotsParent;
    public List<AttractionSlot> slots;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("PC").GetComponent<PlayerChoices>();
    }

    public void LoadFinalScreen()
    {
        foreach (var e in slots)
        {
            Destroy(e.gameObject);
        }
        slots.Clear();
        destination.text = "Your Trip To " + EarthPOI.instance.requestPOI()[pc.destination].name;
        days.text = "Starting: " + pc.tripStartDay[0] + "/" + pc.tripStartDay[1] + "/" + pc.tripStartDay[2] + ", for " +
                    pc.tripDuration + " days";
        plane.text = (pc.planeCost == 0)
            ? "Local Trip: No Plane Tickets"
            : "Plane Ticket: " + pc.planeType + ", $" + pc.planeCost;
        maxBudget.text = "Max Budget: $" + pc.maxBudget + "\nUsed Budget: $" + pc.getUsedBudget() +
            "\nSpending Money: $" + (pc.maxBudget - pc.getUsedBudget());

        if (pc.hotel != null)
        {
            hs.Init(pc.hotel, null);
            hs.gameObject.SetActive(true);
        }
        else
            hs.gameObject.SetActive(false);

        foreach (var entry in pc.attractions)
        {
            GameObject g = Instantiate(attractionPrefab, slotsParent);
            AttractionSlot slot = g.GetComponent<AttractionSlot>();
            slots.Add(slot);
            slot.Init(entry, null, false);
        }
    }
}
