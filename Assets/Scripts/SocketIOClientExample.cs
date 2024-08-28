using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class SocketIOClientExample : MonoBehaviour
{
    public SocketIOUnity socket;
    public string serverUrlLink = "http://localhost:3000";
    void Start()
    {        
        var uri = new Uri(serverUrlLink);       
        socket = new SocketIOUnity(uri); 

        //Create the connection object and bind the lambda function or Action callback:
        socket.OnConnected += (sender, e) => 
        {
            Debug.Log("socket.OnConnected");
        };

        // /Register for the custom event:
        socket.On("message", response =>
        {
            Debug.Log("Event" + response.ToString());
            Debug.Log(response.GetValue<string>());
        });

        //Call the object for connection:
        socket.Connect();
    }
    void Update()
    {       
        if (Input.GetKeyDown(KeyCode.Space))
        {
        socket.EmitAsync("message", "Hello, server!"); // replace with your message
        }
    }   

    void OnDestroy()
    {
        socket.Dispose();
    }
}