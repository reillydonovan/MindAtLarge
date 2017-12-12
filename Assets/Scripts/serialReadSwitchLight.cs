using System;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serialReadSwitchLight : MonoBehaviour {

	//"COM3" will change depending on which USB port Arduino is using
	SerialPort stream = new SerialPort("COM3", 9600);
	private Light nightLight;

	void Start () {
		stream.ReadTimeout = 50;  // ms
		stream.Open();
		nightLight = GetComponent<Light>();
	}

	void Update () {
		try
		{
			string value = stream.ReadLine();
			Debug.Log("received: " + value);
			// string[] data = value.Split(',');
			// return value;
			int state = int.Parse(value);

			if(state == 1)
			{
				nightLight.enabled = true;
			}
			else if(state == 0)
			{
				nightLight.enabled = false;
			}
		}
		catch (TimeoutException)
		{
			// return null;
		}
	}
}
