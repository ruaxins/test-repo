using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
using System.Linq;

public class StartingRequest : BaseRequest
{
    private MainPack isstrat = null;
    public  RoomPanel roomPanel;
    public override void Awake()
    {
        actionCode = ActionCode.Starting;
        base.Awake();
    }
    private void Update()
    {
        if (isstrat != null)
        {
            Debug.Log("¿ªÊ¼ÓÎÏ·");
            face.AddPlayer(isstrat);
            roomPanel.GameStarting(isstrat);
            isstrat = null;
        }
    }
    public override void OnResponse(MainPack pack)
    {
        isstrat = pack;
    }
}
