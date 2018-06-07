using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAudioController : MonoBehaviour
{

    public bool isBookShelfOn = false;
    public bool isTableOn = false;

    public float MaxVolume = 1.0f;
    public float StartingVolume = 0.0f;
    public float RampUpTime = 1;
    private float currentVolume = 0.0f;
    private float lastVolume = 0.0f;
    private float lastVolumeTime = 0.0f;
    private float volumeTimeDelta = 0.2f;

    public AudioSource source;
    // Use this for initialization
    void Start ()
    {
        source.loop = true;
        source.Play();
        source.volume = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float volumeDelta = (MaxVolume / RampUpTime) * Time.deltaTime;

        if (isBookShelfOn || isTableOn)
        {
            currentVolume = Mathf.Min(MaxVolume, currentVolume + volumeDelta);
        }
        else
        {
            currentVolume = Mathf.Max(0, currentVolume - volumeDelta);
        }

        if (Mathf.Abs(lastVolume - currentVolume) > volumeDelta &&
            Mathf.Abs(Time.fixedTime - lastVolumeTime) > volumeTimeDelta)
        {
            source.volume = currentVolume;
            //todo set volume
            lastVolume = currentVolume;
            lastVolumeTime = Time.fixedTime;
        }
	}

    
}
