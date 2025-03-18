using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListRequest : BaseRequest
{
    private MainPack pack = null;
    public RoomPanel roomPanel;
    public override void Awake()
    {
        //requestCode = RequestCode.Room;//只接受不发送
        actionCode = ActionCode.PlayerList;
        base.Awake();
    }
    // Update is called once per frame
    void Update()
    {
       if(pack != null)
       {
            roomPanel.UpdatePlayerList(pack);
            pack = null;
       } 
    }
    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
