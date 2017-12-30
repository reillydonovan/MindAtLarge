using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteLightSwitch : MonoBehaviour
{
    [SerializeField] private bool lightsOn = false;
    
    public void setLightsOn(bool isOn)
    {
        if(isOn != lightsOn)
        {
            updateProbes();
        }
        lightsOn = isOn;
    }

    public GameObject onLightParent;
    public GameObject offLightParent;

    public GameObject ReflectionProbeParent;


    void Start ()
    {
        //TODO establish arduino connection
    }
	

	void Update()
    {
        GetLightStatusFromArduino();
        SwitchLights();
    }

    void SwitchLights()
    {

        if (onLightParent != null)
        {
            onLightParent.SetActive(lightsOn);
        }

        if (offLightParent != null)
        {
            offLightParent.SetActive(!lightsOn);
        }
    }

    void GetLightStatusFromArduino()
    {
        //TODO: get physical arduino light status
    }

    void updateProbes()
    {
        foreach(ReflectionProbe probe in ReflectionProbeParent.GetComponentsInChildren<ReflectionProbe>())
        {
            probe.RenderProbe();
        }
    }
}
