using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public GameObject playerinfoitem;
    public Transform listTransform;
    public Text timetext;
    public Button exitBtn;
    public GameExitRequest gameExitRequest;

    private float starttime;

    private Dictionary<string,PlayerInfo> itemlist = new Dictionary<string,PlayerInfo>();

    private void Start()
    {
        starttime = Time.time;
        exitBtn.onClick.AddListener(OnClickExit);
    }
    public void UpdateList(MainPack packs)//更新玩家列表的玩家数量
    {
        for (int i = 0; i < listTransform.childCount; i++)
        {
            GameObject.Destroy(listTransform.GetChild(i).gameObject);
        }
        itemlist.Clear();
        foreach (var p in packs.Playerpack)
        {
            GameObject g = Instantiate(playerinfoitem,Vector3.zero,Quaternion.identity);
            g.transform.SetParent(listTransform);
            PlayerInfo pInfo = g.GetComponent<PlayerInfo>();
            pInfo.Set(p.Playername,p.Hp);
            itemlist.Add(p.Playername, pInfo);//存储ID和玩家信息类
        }
    }
    public void UpdateHp(string id,int v)
    {
        if(itemlist.TryGetValue(id, out PlayerInfo pInfo))
        {
            pInfo.Up(v);
        }
        else
        {
            Debug.Log("获取不到角色血量信息");
        }

    }

    private void FixedUpdate()
    {
        timetext.text = Mathf.Clamp((int)(Time.time - starttime),0,300).ToString();
    }
    private void OnClickExit()
    {
        gameExitRequest.SendRequest();
        face.GameExit();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Enter();
    }
    public override void OnPause()
    {
        base.OnPause();
        Exit();
    }
    public override void OnRecovery()
    {
        base.OnRecovery();
        Enter();
    }
    public override void OnExit()
    {
        base.OnExit();
        Exit();
    }
    private void Enter()
    {
        gameObject.SetActive(true);
    }
    private void Exit()
    {
        gameObject.SetActive(false);
    }
}
