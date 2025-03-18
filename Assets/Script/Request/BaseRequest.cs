using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode;
    protected ActionCode actionCode;
    protected GameFace face;

    public ActionCode GetActionCode { get { return actionCode; } }

    public virtual void Awake()
    {
        face = GameFace.Face;
        
    }
    private void Start()
    {
        face.AddRequest(this);
    }
    public virtual void OnDestroy()
    {
        face.RemoveRequest(actionCode);
    }
    public virtual void OnResponse(MainPack pack)
    {
        //ผฬณะ
    }
    public virtual void SendRequest(MainPack pack)
    {
        face.Send(pack);
    }
    public virtual void SendRequestUDP(MainPack pack)
    {
        face.SendTo(pack);
    }
}
