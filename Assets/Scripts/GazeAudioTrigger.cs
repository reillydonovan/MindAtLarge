using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
class GazeAudioTrigger : GazeReceiver
{
    public AudioClip clip;
    private AudioSource source;
    private RemoteAudioTrigger remoteAudioTrigger = new RemoteAudioTrigger();

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    protected override void GazeEntryTriggerOnce(RaycastHit hit)
    {

    }

    protected override void GazeDelayTriggerOnce(RaycastHit hit)
    {
        Debug.Log("running your stupid code");
        source.clip = clip;
        source.Play();
    }

    protected override void GazeUpdate(RaycastHit hit)
    {

    }

}

