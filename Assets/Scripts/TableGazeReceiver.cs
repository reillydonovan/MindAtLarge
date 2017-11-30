using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableGazeReceiver : GazeReceiver
{
    public RemoteAudioTrigger Audiotrigger;

    public float MaxVolume = 1.0f;
    public float StartingVolume = 0.0f;
    public float RampUpTime = 1;

    private float currentVolume = 0.0f;
    private float lastVolume = 0.0f;
    private float volumeUpdateDelta = 0.1f;
    private float lastVolumeTime = 0.0f;
    private float volumeTimeDelta = 0.2f;
    private bool isGazedOn = false;

	// Use this for initialization
	void Start ()
    {
        currentVolume = StartingVolume;
    }

    void Update()
    {
        base.Update();
        float volumeDelta = (MaxVolume / RampUpTime) * Time.deltaTime;

        if (isGazedOn)
        {
            currentVolume = Mathf.Min(MaxVolume, currentVolume + volumeDelta);
        }
        else
        {
            currentVolume = Mathf.Max(0, currentVolume - volumeDelta);
        }
            
        if( Mathf.Abs(lastVolume-currentVolume) > volumeDelta && 
            Mathf.Abs(Time.fixedTime- lastVolumeTime) > volumeTimeDelta)
        {
            Audiotrigger.setTrackAndVolume("monteverdi", currentVolume);
            lastVolume = currentVolume;
            lastVolumeTime = Time.fixedTime;
        }
    }
    protected override void GazeEntryTriggerOnce(RaycastHit hit)
    {
        isGazedOn = true;
        Debug.Log("TableGazeReceiver GazeEntryTriggerOnce");
       // Audiotrigger.setTrackAndVolume("monteverdi", MaxVolume);
    }

    ////ramp up the 
    //protected override void GazeUpdate(RaycastHit hit)
    //{
    //    Debug.Log("TableGazeReceiver GazeUpdate");
    //}
    protected override void GazeExit()
    {
        isGazedOn = false;
        Debug.Log("TableGazeReceiver GazeExit");
        //Audiotrigger.SendVolume(0);
    }
}
