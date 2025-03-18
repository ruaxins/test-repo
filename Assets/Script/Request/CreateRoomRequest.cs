using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomRequest : BaseRequest
{
    MainPack mainPack = null;
    public RoomListPanel roomListPanel;
    public override void Awake()
    {
        requestCode=RequestCode.Room;
        actionCode=ActionCode.CreateRoom;
        base.Awake();
    }
    private void Update()
    {
        if(mainPack != null)
        {
            //Debug.Log("mainpack��Ϊ��" + mainPack);
            roomListPanel.CreateRoomResponse(mainPack);
            mainPack = null;
        }
    }
    public override void OnResponse(MainPack pack)
    {
        mainPack = pack;
    }

    public void SendRequest(string roomname,int maxnum)
    {
        //Debug.Log("���ʹ�������"+roomname);
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        RoomPack room = new RoomPack();
        room.Roomname = roomname;
        room.Maxnum = maxnum;
        pack.Roompack.Add(room);
        base.SendRequest(pack);
    }
}
