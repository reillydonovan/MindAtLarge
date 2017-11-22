using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteLightSwitch : MonoBehaviour
{
    public bool lightsOn = false;

    public GameObject onLightParent;
    public GameObject offLightParent;

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
}
