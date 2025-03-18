using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class RoomExitRequest : BaseRequest
{
    private bool isexit = false;
    public RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.ExitRoom;
        base.Awake();
    }
    public void SendRequest()
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        pack.Str = "r";
        base.SendRequest(pack);
    }
    private void Update()
    {
        if(isexit == true)
        {
            //Debug.Log("房主退出"+isexit);
            isexit = false;
            roomPanel.ExitRoomResponse();
            
        }
    }
    public override void OnResponse(MainPack pack)
    {
        //Debug.Log("接收响应:"+pack.Actioncode);
        isexit=true;
    }
}
