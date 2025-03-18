using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class FireRequest : BaseRequest
{
    MainPack pack = null;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Fire;
        base.Awake();
    }
    private void Update()
    {
        if(pack != null)
        {
            face.SpawnBullet(pack);
            pack = null;
        }
    }
    public void SendRequest(Vector3 pos,float rot,Vector2 mpos)
    {
        MainPack pack = new MainPack();
        BulletPack bulletpack = new BulletPack();
        bulletpack.PosX = pos.x;
        bulletpack.PosY = pos.y;
        bulletpack.MPosX = mpos.x;
        bulletpack.MPosY = mpos.y;
        bulletpack.RosZ = rot;
        pack.Bulletpack = bulletpack;
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        base.SendRequestUDP(pack);
    }
    public override void OnResponse(MainPack pack)
    {
        this.pack = pack;
    }
}
