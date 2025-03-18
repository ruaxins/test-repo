using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRoomRequest : BaseRequest
{
    MainPack mainPack = null;
    public RoomListPanel roomListPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.FindRoom;
        base.Awake();
    }
    private void Update()
    {
        if (mainPack != null)
        {
            roomListPanel.FindRoomResponse(mainPack);
            mainPack = null;
        }
    }
    public override void OnResponse(MainPack pack)
    {
        mainPack = pack;
    }

    public void SendRequest()
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        pack.Str = "r";
        base.SendRequest(pack);
    }
}
