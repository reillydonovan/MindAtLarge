using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RemoteAudioTriggerUdp : MonoBehaviour {

	private string CurrentTrack = null;
	private float CurrentVolume = -1.0f;
    [Tooltip("if no ip addresses are listed the script will attempt to use udp broadcast")] public string[] ipAddresses; 
	private UdpClient socket;

    //list ip endpoints (raspberry pi ip addresses)
    List<IPEndPoint> ips = new List<IPEndPoint>();
    void Start ()
    {
        //add all of the ip addresses to the list
        if (ipAddresses.Length > 0)
        {
            foreach (string ipAddr in ipAddresses)
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ipAddr), 9999);
                ips.Add(ip);
            }
        }
        else
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, 9999);
            ips.Add(ip);
        }
        // Debug.Log("Initializing UDP broadcast socket at " + ip);
        // socket = new UdpClient();
        // Debug.Log("....Done Initializing");
    }

	void Update () { }

	private void SendData(string trackName, float volumelevel, string hostname)
	{
		if(String.IsNullOrEmpty(trackName) || volumelevel < 0 )
		{ Debug.LogError("Attempting to send invalid trackname or volume!"); return; }
		try
		{
			socket = new UdpClient();
			// IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, 9999);

			string writeString = hostname + "/" + trackName + "/" + volumelevel;
			byte[] bytes = Encoding.ASCII.GetBytes(writeString);

            foreach (IPEndPoint ip in ips)
            {
                Debug.Log("sending track: " + trackName + " at volume: " + volumelevel + " to " + ip + "\n  writeString: " + writeString);
                socket.Send(bytes, bytes.Length, ip);
            }
			socket.Close();
		}
		catch (Exception e)
		{ Debug.Log("Socket error:" + e); }
	}

	public void SendVolume(float volume, string hostName)
	{
		SendData(CurrentTrack, volume, hostName);
		CurrentVolume = volume;
	}

	public void SendTrackName(string trackName, string hostName)
	{
		SendData(trackName, CurrentVolume, hostName);
		CurrentTrack = trackName;
	}

	public void setTrackAndVolume(string trackName, float volumelevel, string hostName)
	{
		SendData(trackName, volumelevel, hostName);
		CurrentVolume = volumelevel;
		CurrentTrack = trackName;
	}

	public void SendTrackPosition(float normalizedPosition)
	{
		//todo
	}
		
	public void OnDestroy()
	{
		Debug.Log("Remote Audio Trigger OnDestroy");
		//SendData(CurrentTrack, 0, hostname);
	}
}
