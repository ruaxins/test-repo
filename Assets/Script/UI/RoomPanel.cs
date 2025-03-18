using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    public Button back, send, start;
    public InputField inputtext;
    public Scrollbar scrollbar;
    public Text chattext;
    public Transform content;
    public GameObject useritemobj;
    public RoomExitRequest roomExitRequest;
    public ChatRequest chatrequest;
    public StartGameRequest startgamerequest;

    private void Start()
    {
        back.onClick.AddListener(OnBackClick);
        send.onClick.AddListener(OnSendClick);
        start.onClick.AddListener(OnStartClick);
    }
    private void OnBackClick()
    {
        //Debug.Log("RoomBackPop");
        roomExitRequest.SendRequest();
        //uiManager.PopPanel();
    }
    private void OnSendClick()
    {
        if(chattext == null)
        {
            uiManager.ShowMessage("�������ݲ���Ϊ��");
            return;
        }
        chatrequest.SendRequest(inputtext.text);
        chattext.text += "��:" + inputtext.text + "\n";
        inputtext.text = null;
    }
    private void OnStartClick()
    {
        startgamerequest.SendRequest();
    }

    public void UpdatePlayerList(MainPack pack)//���·����������Ϣ
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        foreach (PlayerPack player in pack.Playerpack)
        {
            UserItem userItem = Instantiate(useritemobj,Vector3.zero,Quaternion.identity).GetComponent<UserItem>();
            userItem.gameObject.transform.SetParent(content);
            userItem.SetPlayerInfo(player.Playername);
        }
    }
    public void ExitRoomResponse()
    {
        uiManager.PopPanel();
    }

    public void ChatResponse(string str)
    {
        chattext.text += str+"\n";
    }

    public void StartGameResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.ReturnNone:
                Debug.LogWarning("δ��ʼ");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("��Ϸ��������");
                isStart = true;
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("ֻ�з������Կ�ʼ��Ϸ");
                break;
            default:
                Debug.LogWarning("��ʼ����");
                break;
        }
    }
    bool isStart = false;
    float time = 0;
    int index = 5;
    private void Update()//��ʱ
    {
        if (isStart)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
            index = 5;
        }
        if (time > 1 && index > 0)
        {
            uiManager.ShowMessage(index.ToString());
            index--;
            time = 0;
        }
        else if (index <= 0)
        {
            isStart = false;
        }
    }
    public void GameStarting(MainPack packs)
    {
        GamePanel gamePanel = uiManager.PushPanel(PanelType.Game)as GamePanel;
        gamePanel.UpdateList(packs);
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
