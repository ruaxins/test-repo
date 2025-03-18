using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using SocketGameProtocol;
using Unity.VisualScripting.Antlr3.Runtime;

public class Client : MonoBehaviour
{
    //例子脚本，暂无使用

    Socket socket;
    byte[] buffer = new byte[1024];

    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect("127.0.0.1",6666);
        StartReceive();
        Send();
    }

    void StartReceive()
    {
        socket.BeginReceive(buffer,0,buffer.Length,SocketFlags.None, ReceiveCallBack, null);
    }

    void ReceiveCallBack(IAsyncResult iar)
    {
        int len = socket.EndReceive(iar);
        if (len == 0) return;
        string str = Encoding.UTF8.GetString(buffer,0,len);
        StartReceive();
    }

    void Send()
    {
        socket.Send(Encoding.UTF8.GetBytes("你好"));
    }
    void Update()
    {
        
    }
}
