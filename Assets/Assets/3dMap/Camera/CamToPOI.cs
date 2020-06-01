using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamToPOI : MonoBehaviour
{
    public float rotSpeed;
    [HideInInspector]
    public float r2Spd;
    public float r2A;
    public Transform pivot;
    [HideInInspector]
    public Quaternion targetRot;
    [HideInInspector]
    public float targetDist;
    public bool controlOn = false;
    public Animator SHUTOFF;

    [Space] public Transform outerParent;
    public Transform spinParent;

   // private bool grabbedCanvas;
    //public Transform leftSideCanvas;
    private void Update()
    {
        if (controlOn)
        {
            //if (!grabbedCanvas)
               // leftSideCanvas.parent = transform;
            SHUTOFF.enabled = false;
            if(Quaternion.Angle(pivot.localRotation,targetRot)>0.01f)
                pivot.localRotation = Quaternion.Lerp(pivot.localRotation, targetRot, Mathf.Clamp(Time.deltaTime * r2Spd,0,0.99f));

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                Mathf.Lerp(transform.localPosition.z, targetDist, Time.deltaTime * r2Spd));
            if(r2Spd<50)
            r2Spd += Time.fixedDeltaTime * r2A;
        }
    }

    public void SetTargetRot(Quaternion q)
    {
        Vector3 f = q * Vector3.forward;
        q = Quaternion.LookRotation(f, Vector3.up);
        targetRot = q;
        r2Spd = rotSpeed;
    }
    
    public void SetTargetDist(float d)
    {
        targetDist = d;
    }

    public void SpinWithParent(bool t)
    {
        if (t)
            pivot.parent = spinParent;
        else
            pivot.parent = outerParent;
        r2Spd = rotSpeed;
    }
}
