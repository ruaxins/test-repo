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
    /// ע����¼
    /// </summary>
    private void OnBackClick()
    {
        Debug.Log("RoomListPanelPop");
        uiManager.PopPanel();
    }
    /// <summary>
    /// ��ѯ����
    /// </summary>
    private void OnFindClick()
    {
        findRoomRequest.SendRequest();
    }
    /// <summary>
    /// ��������
    /// </summary>
    private void OnCreateClick()
    {
        if(roomname.text ==  "")
        {
            uiManager.ShowMessage("����������Ϊ��");
            return;
        }
        createRoomRequest.SendRequest(roomname.text,(int)num.value);
    }
    public void CreateRoomResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.ReturnNone:
                Debug.LogWarning("δ����");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("�����ɹ�");
                //uiManager.PushPanel(PanelType.Room);
                BasePanel roomPanel = uiManager.PushPanel(PanelType.Room);
                //Debug.Log(roomPanel);
                ((RoomPanel)roomPanel).UpdatePlayerList(pack);
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("����ʧ��");
                break;
            default:
                Debug.LogWarning("��������");
                break;
        }
    }
    public void FindRoomResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.ReturnNone:
                Debug.LogWarning("δ��ѯ");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("��ѯ�ɹ�");
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("��ѯʧ��");
                break;
            default:
                Debug.LogWarning("��ѯ����");
                break;
        }
        UpadteRoomList(pack);
    }
    private void UpadteRoomList(MainPack pack)
    {
        //����б�
        for (int i = 0; i < roomListTransform.childCount; i++)
        {
            Destroy(roomListTransform.GetChild(i).gameObject);
        }
        foreach (RoomPack room in pack.Roompack)
        {
            RoomItem item = Instantiate(roomItem,Vector3.zero,Quaternion.identity).GetComponent<RoomItem>();//ʵ����һ��roomitem����ȡ���
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
                Debug.LogWarning("δ����");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("����ɹ�");
                RoomPanel roomPanel = uiManager.PushPanel(PanelType.Room) as RoomPanel;
                roomPanel.UpdatePlayerList(pack);
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("����ʧ��");
                break;
            default:
                Debug.LogWarning("��������");
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
