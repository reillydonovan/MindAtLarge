using System;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class serialReadSwitchLight : MonoBehaviour
{

    [SerializeField] string PortNameString = "COM3";

    public RemoteLightSwitch LightSwitch;
    private ArduinoSwitchReader switchReader;

    void Start ()
    {
        switchReader = new ArduinoSwitchReader(PortNameString);
    }

	void Update ()
    {
        LightSwitch.setLightsOn(switchReader.isOn);
	}

    private void OnDestroy()
    {
        switchReader.Abort();
    }
}

class ArduinoSwitchReader
{
    string PortNameString;
    private System.Threading.Thread ReaderThread = null;
    SerialPort stream;
    public bool isOn;
    private bool shouldRun;


    public ArduinoSwitchReader(string portString)
    {
        PortNameString = portString;

        shouldRun = true;
        ReaderThread = new System.Threading.Thread(Run);
        ReaderThread.IsBackground = true;
        ReaderThread.Start();
    }

    /// <summary>
    /// conclusion of thread running    
    /// </summary>
    public void Abort()
    {
        shouldRun = false;
        ReaderThread.Join();
        stream.Close();
    }

    /// <summary>
    /// encapsulation of the thread run op - init, loop
    /// </summary>
    private void Run()
    {
        stream = new SerialPort(PortNameString, 9600);
        stream.ReadTimeout = 50;  // milliseconds
        stream.Open();
        //run loop
        while (stream.IsOpen && shouldRun)
        {
            readSwitch();
        }
        stream.Close();
    }

    /// <summary>
    /// Read op
    /// </summary>
    private void readSwitch()
    {
        try
        {
            string value = stream.ReadLine();
            Debug.Log("received: " + value);
            int state = int.Parse(value);

            isOn = (state == 1);
        }
        catch (TimeoutException)
        {
            // return null;
        }
    }
}