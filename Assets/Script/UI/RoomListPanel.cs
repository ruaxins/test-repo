using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketGameProtocol;

public class RoomListPanel : BasePanel
{
    public Button back, find, create;
    public InputField roomname;

    public Transform roomListTransform;
    public GameObject roomItem;
    public Slider num;

    public CreateRoomRequest createRoomRequest;
    public FindRoomRequest findRoomRequest;
    public JoinRoomRequest joinRoomRequest;

    private void Start()
    {
        back.onClick.AddListener(OnBackClick);
        find.onClick.AddListener(OnFindClick);
        create.onClick.AddListener(OnCreateClick);
    }

    /// <summary>
    /// 注销登录
    /// </summary>
    private void OnBackClick()
    {
        Debug.Log("RoomListPanelPop");
        uiManager.PopPanel();
    }
    /// <summary>
    /// 查询房间
    /// </summary>
    private void OnFindClick()
    {
        findRoomRequest.SendRequest();
    }
    /// <summary>
    /// 创建房间
    /// </summary>
    private void OnCreateClick()
    {
        if(roomname.text ==  "")
        {
            uiManager.ShowMessage("房间名不能为空");
            return;
        }
        createRoomRequest.SendRequest(roomname.text,(int)num.value);
    }
    public void CreateRoomResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.ReturnNone:
                Debug.LogWarning("未创建");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("创建成功");
                //uiManager.PushPanel(PanelType.Room);
                BasePanel roomPanel = uiManager.PushPanel(PanelType.Room);
                //Debug.Log(roomPanel);
                ((RoomPanel)roomPanel).UpdatePlayerList(pack);
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("创建失败");
                break;
            default:
                Debug.LogWarning("创建意外");
                break;
        }
    }
    public void FindRoomResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.ReturnNone:
                Debug.LogWarning("未查询");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("查询成功");
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("查询失败");
                break;
            default:
                Debug.LogWarning("查询意外");
                break;
        }
        UpadteRoomList(pack);
    }
    private void UpadteRoomList(MainPack pack)
    {
        //清空列表
        for (int i = 0; i < roomListTransform.childCount; i++)
        {
            Destroy(roomListTransform.GetChild(i).gameObject);
        }
        foreach (RoomPack room in pack.Roompack)
        {
            RoomItem item = Instantiate(roomItem,Vector3.zero,Quaternion.identity).GetComponent<RoomItem>();//实例化一个roomitem并获取组件
            item.roomListPanel = this;
            item.gameObject.transform.SetParent(roomListTransform);
            item.SetRoomItemInfo(room.Roomname,room.Currentnum,room.Maxnum,room.Status);
        }
    }

    public void JoinRoomResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.ReturnNone:
                Debug.LogWarning("未加入");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("加入成功");
                RoomPanel roomPanel = uiManager.PushPanel(PanelType.Room) as RoomPanel;
                roomPanel.UpdatePlayerList(pack);
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("加入失败");
                break;
            default:
                Debug.LogWarning("加入意外");
                break;
        }
    }
    public void JoinRoom(string roomname)
    {
        joinRoomRequest.SendRequest(roomname);
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
