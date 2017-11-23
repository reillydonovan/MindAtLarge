using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGazeReciever : GazeReceiver {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void GazeEntryTriggerOnce(RaycastHit hit)
    {
        Debug.Log("FlowerGazeReciever GazeEntryTriggerOnce");
    }

    protected override void GazeDelayTriggerOnce(RaycastHit hit)
    {
        Debug.Log("FlowerGazeReciever GazeDelayTriggerOnce");
    }

    protected override void GazeUpdate(RaycastHit hit)
    {
        Debug.Log("FlowerGazeReciever GazeUpdate");
    }
}
