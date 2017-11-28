using System;

using System.IO;

using System.Net.Sockets;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class UnitySocketSend : MonoBehaviour
{



    private String host = "192.168.1.112";

    private Int32 port = 9999;

    TcpClient socket;

    public NetworkStream stream;

    StreamWriter writer;

    //StreamReader reader;



    void Start()
    {



        try
        {

            socket = new TcpClient(host, port);

            stream = socket.GetStream();

            writer = new StreamWriter(stream);

            //reader = new StreamReader(stream);

            writer.Write("monteverdi/0.8");

            writer.Flush();

        }

        catch (Exception e)
        {

            Debug.Log("Socket error:" + e);

        }

    }



    void Update()
    {



    }

}