using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableGazeReceiver : GazeReceiver
{
    private RemoteAudioTrigger audiotrigger;

    public float MaxVolume = 1.0f;
    public float StartingVolume = 0.0f;
    public float RampUpTime = 1;

    private float currentVolume = 0.0f;

	// Use this for initialization
	void Start ()
    {
        audiotrigger = new RemoteAudioTrigger();
        currentVolume = StartingVolume;

    }

    void Update()
    {
        base.Update();

    }
    protected override void GazeEntryTriggerOnce(RaycastHit hit)
    {
        Debug.Log("TableGazeReceiver GazeEntryTriggerOnce");
        audiotrigger.setTrackAndVolume("monteverdi", MaxVolume);
    }

    //ramp up the 
    protected override void GazeUpdate(RaycastHit hit)
    {
        Debug.Log("TableGazeReceiver GazeUpdate");
    }
    protected override void GazeExit()
    {
        Debug.Log("TableGazeReceiver GazeExit");
        audiotrigger.SendVolume(0);
    }
}
