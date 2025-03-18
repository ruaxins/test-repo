using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using SocketGameProtocol;
using System.Net;
using System.Threading;

public class ClientManager : BaseManager
{
    private Socket socket;
    private Message message;
    private Thread aucThread;
    private const string ip = "127.0.0.1";
    public ClientManager(GameFace face) : base(face) { }

    public override void OnInit()
    {
        base.OnInit();
        message = new Message();
        InitSocket();
        InitUDP();
    }
    public override void OnDestory()
    {
        base.OnDestory();
        message = null;
        CloseSocket();
        if(aucThread != null)
        {
            aucThread.Abort();
            aucThread = null;
        }
    }
    /// <summary>
    /// 初始化Socket
    /// </summary>
    private void InitSocket()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect(ip, 6666);//连接服务器主机(本地)IP和端口
            StartReceive();
            face.ShowMessage("连接成功");
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);//连接失败
            face.ShowMessage("连接失败");
        }
    }
    /// <summary>
    /// 关闭Socket
    /// </summary>
    private void CloseSocket()
    {
        if (socket.Connected && socket != null)
        {
            socket.Close();
        }
    }
    private void StartReceive()
    {
        socket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None,ReceiveCallBack, null);
    }
    private void ReceiveCallBack(IAsyncResult iar)
    {
        try
        {
            if (socket == null || socket.Connected == false) return;
            int len = socket.EndReceive(iar);
            if(len == 0)
            {
                CloseSocket();
                return;
            }
            message.ReadBuffer(len, HandleResponse);   
            StartReceive();
        }
        catch
        {

            
        }
    }
    private void HandleResponse(MainPack pack)
    {
        face.HandleResponse(pack);
    }
    public void Send(MainPack pack)
    {
        //Debug.Log("成功向服务器发送pack包");
        socket.Send(Message.PackData(pack));
    }

    //UDP
    private Socket udpSocket;
    private IPEndPoint ipEndPoint;
    private EndPoint endPoint;
    private byte[] buffer = new byte[1024];

    private void InitUDP()
    {
        udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), 6667);
        endPoint = (EndPoint)ipEndPoint;
        try
        {
            udpSocket.Connect(endPoint);
        }
        catch
        {
            Debug.Log("连接失败");
            return;
        }
        aucThread = new Thread(ReceiveMsg);
        aucThread.Start();
    }
    private void ReceiveMsg()
    {
        while (true)
        {
            int len = udpSocket.ReceiveFrom(buffer, ref endPoint);
            MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer,0,len);
            HandleResponse(pack);
        }
    }
    public void SendTo(MainPack pack)
    {
        //PlayerPack p1 = pack.Playerpack[0];
        //Debug.Log(pack.User+":"+p1.Pospack);
        byte[] buff = Message.PackDataUDP(pack);
        udpSocket.Send(buff,buff.Length,SocketFlags.None);
    }
}
