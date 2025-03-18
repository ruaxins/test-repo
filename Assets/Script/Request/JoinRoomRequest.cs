using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomRequest : BaseRequest
{
    MainPack pack = null;
    public RoomListPanel roomListPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.JoinRoom;
        base.Awake();
    }
    public void SendRequest(string roomname)
    {
        MainPack mainpack = new MainPack();
        mainpack.Requestcode = requestCode;
        mainpack.Actioncode = actionCode;
        mainpack.Str = roomname;
        base.SendRequest(mainpack);
    }
    void Update()
    {
        if (pack != null)
        {
            roomListPanel.JoinRoomResponse(pack);
            pack = null;
        }
    }
    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
