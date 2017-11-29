using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteAudioTrigger
{
    public String Host = "10.0.0.9";
    public Int32 Port = 9999;
    private string CurrentTrack = "";
    private float CurrentVolume = 0.0f;


    public RemoteAudioTrigger()
    {
        //todo add init details
    }

    private void SendData(string trackName, float volumelevel)
    {
        try
        {
            //Should these be initilaized once or everytime we send a message?
            TcpClient socket = new TcpClient(Host, Port);
            NetworkStream stream = socket.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            //writer.Write("monteverdi/0.8");
            writer.Write( trackName + "/"+ volumelevel );
            writer.Flush();
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

}
