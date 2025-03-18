using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class UpdatePosRequest : BaseRequest
{
    MainPack mainPack = null;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.UpdatePos;
        base.Awake();
    }
    private void Update()
    {
        if(mainPack != null)
        {
            face.UpdatePos(mainPack);
            mainPack = null;
        }
    }
    public void SendRequest(Vector3 pos,float characterRot,float GunRotX, float GunRotY)
    {
        MainPack pack = new MainPack();
        PosPack posPack = new PosPack();
        PlayerPack playerPack = new PlayerPack();
        posPack.PosX = pos.x;
        posPack.PosY = pos.y;
        posPack.PosZ = characterRot;
        posPack.GunRotX = GunRotX;
        posPack.GunRotY = GunRotY;
        playerPack.Playername = face.UserName;
        playerPack.Pospack = posPack;
        pack.Playerpack.Add(playerPack);
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        base.SendRequestUDP(pack);
    }
    public override void OnResponse(MainPack pack)
    {
        mainPack = pack;
    }
}
