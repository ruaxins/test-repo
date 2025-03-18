using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameFace : MonoBehaviour
{
    private ClientManager clientManager;
    private RequestManager requestManager;
    private UIManager uiManager;
    private PlayerManager playerManager;

    private static GameFace face;

    public static GameFace Face
    {
        get
        {
            if (face == null)
            {
                face = GameObject.Find("GameFace").GetComponent<GameFace>();
            }
            return face;
        }
    }

    public string UserName { get; set; }

    void Awake()
    {
        uiManager = new UIManager(this);
        clientManager = new ClientManager(this);
        requestManager = new RequestManager(this);
        playerManager = new PlayerManager(this);
        
        uiManager.OnInit();
        clientManager.OnInit();
        requestManager.OnInit();
        playerManager.OnInit();
    }

    private void OnDestroy()
    {
        clientManager.OnDestory();
        requestManager.OnDestory();
        uiManager.OnDestory();
        playerManager.OnDestory();
    }

    public void Send(MainPack pack)
    {
        clientManager.Send(pack);
    }
    public void SendTo(MainPack pack)
    {
        pack.User = UserName;
        clientManager.SendTo(pack);
    }
    public void HandleResponse(MainPack pack)
    {
        requestManager.HandleResponse(pack);    
    }
    public void AddRequest(BaseRequest request)
    {
        requestManager.AddRequest(request);
    }
    public void RemoveRequest(ActionCode action)
    {
        requestManager.RemoveRequest(action);
    }
    public void ShowMessage(string str,bool sync = false)
    {
        uiManager.ShowMessage(str,sync);
    }
    public void AddPlayer(MainPack packs)
    {
        playerManager.AddPlayer(packs);
    }
    public void RemovePlayer(string id)
    {
        playerManager.RemovePlayer(id);
    }
    public void GameExit()
    {
        playerManager.GameExit();
        uiManager.PopPanel();
        uiManager.PopPanel();
    }
    public void UpdatePos(MainPack pack)
    {
        playerManager.UpdatePos(pack);
    }
    public void SpawnBullet(MainPack pack)
    {
        playerManager.SpawnBullet(pack);
    }
}
