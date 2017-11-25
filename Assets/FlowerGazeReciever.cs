﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGazeReciever : GazeReceiver {

    Material myMaterial;

    void Start ()
    {
        
        myMaterial = GetComponent<MeshRenderer>().materials[1];
        
        if(myMaterial == null)
        {
            Debug.LogError("We got a problem");
        }
    }

    protected override void GazeEntryTriggerOnce(RaycastHit hit)
    {
    }

    protected override void GazeDelayTriggerOnce(RaycastHit hit)
    {
    }

    protected override void GazeUpdate(RaycastHit hit)
    {
        string propName = "_Factor";
        if (myMaterial.HasProperty(propName))
        {
            float modulator = 0.1f*Mathf.Sin(Time.time * 4.0f);
            myMaterial.SetFloat(propName, modulator);
        }
        else
        {
            Debug.Log("Material missing \"" + propName + "\"property");
        }
    }
}
