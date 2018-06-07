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
    private float volumeTimeDelta = 0.1f;

    public AudioSource source;
    // Use this for initialization
    void Start ()
    {
        Debug.Log("started the audio business...");
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
			Debug.Log("scaling down computer audio");
			currentVolume = Mathf.Max(0, currentVolume - volumeDelta);
        }
        else
        {
			Debug.Log("ramping up computer audio");
			currentVolume = Mathf.Min(MaxVolume, currentVolume + volumeDelta);
        }

        if (Mathf.Abs(lastVolume - currentVolume) > volumeDelta &&
            Mathf.Abs(Time.fixedTime - lastVolumeTime) > volumeTimeDelta)
        {
            Debug.Log("current level set at: " + currentVolume);
            source.volume = currentVolume;
            //todo set volume
            lastVolume = currentVolume;
            lastVolumeTime = Time.fixedTime;
        }
	}

    
}
