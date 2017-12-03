using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteAudioTrigger: MonoBehaviour
{
    public String Host = "192.168.1.112";
    public Int32 Port = 9999;
    private string CurrentTrack = null;
    private float CurrentVolume = -1.0f;

    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;

    public void Start()
    {
        Debug.Log("Initializing socket stream and writer " + Host + " : " + Port);
        socket = new TcpClient(Host, Port);
        stream = socket.GetStream();
        writer = new StreamWriter(stream);
        Debug.Log("....Done Initializing");
    }

    public void Update()
    {
        
    }

    private void printme()
    {
        Debug.Log(" socket: " + socket.ToString() + " stream: "  + stream.ToString() + " writer: " + writer.ToString());
        Debug.Log("CurrentTrack: " + CurrentTrack + " CurrentVolume: " + CurrentVolume + " Host: " + Host + " Port: " + Port);

    }               

    private void SendData(string trackName, float volumelevel)
    {
        if(String.IsNullOrEmpty(trackName) || volumelevel < 0 )
        { Debug.LogError("Attempting to send invalid trackname or volume!"); return; }
        printme();
        try
        {
            socket = new TcpClient(Host, Port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);

            //Should these be initilaized once or everytime we send a message?

            //writer.Write("monteverdi/0.8");
            string writeString = "" + trackName + "/" + volumelevel;
            Debug.Log("sending track: " + trackName + " at volume: " + volumelevel + " to " + Host + " : " + Port + "\n  writeString: " + writeString);
            writer.Write( writeString);
            writer.Flush();
            stream.Close();
            writer.Close();

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
        stream.Close();
        writer.Close();
        socket.Close();

    }
}
