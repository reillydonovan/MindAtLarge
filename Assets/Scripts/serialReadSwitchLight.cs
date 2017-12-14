using System;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serialReadSwitchLight : MonoBehaviour {

    [SerializeField] string PortNameString = "COM3";
    SerialPort stream;
    public RemoteLightSwitch LightSwitch;

	void Start () {
        stream = new SerialPort(PortNameString, 9600);
        stream.ReadTimeout = 50;  // ms
		stream.Open();
	}

	void Update () {
		try
		{
			string value = stream.ReadLine();
			Debug.Log("received: " + value);
			// string[] data = value.Split(',');
			// return value;
			int state = int.Parse(value);

            bool isOn = (state == 1);
            LightSwitch.lightsOn = isOn;
		}
		catch (TimeoutException)
		{
			// return null;
		}
	}
}
