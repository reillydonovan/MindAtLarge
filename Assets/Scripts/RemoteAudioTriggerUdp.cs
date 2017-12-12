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

	private UdpClient socket;
	public IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, 9999);

	void Start () {
		// Debug.Log("Initializing UDP broadcast socket at " + ip);
		// socket = new UdpClient();
		// Debug.Log("....Done Initializing");
	}

	void Update () {
		
	}

	private void SendData(string trackName, float volumelevel)
	{
		if(String.IsNullOrEmpty(trackName) || volumelevel < 0 )
		{ Debug.LogError("Attempting to send invalid trackname or volume!"); return; }
		try
		{
			socket = new UdpClient();
			// IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, 9999);

			string writeString = "" + trackName + "/" + volumelevel;
			byte[] bytes = Encoding.ASCII.GetBytes(writeString);

			Debug.Log("sending track: " + trackName + " at volume: " + volumelevel + " to " + ip + "\n  writeString: " + writeString);
			socket.Send(bytes, bytes.Length, ip);

			socket.Close();
		}
		catch (Exception e)
		{ Debug.Log("Socket error:" + e); }
	}

	public void SendVolume(float volume)
	{
		SendData(CurrentTrack, volume);
		CurrentVolume = volume;
	}

	public void SendTrackName(string trackName)
	{
		SendData(trackName, CurrentVolume);
		CurrentTrack = trackName;
	}

	public void setTrackAndVolume(string trackName, float volumelevel)
	{
		SendData(trackName, volumelevel);
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
		SendData(CurrentTrack, 0);
	}
}
