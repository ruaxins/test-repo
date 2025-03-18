using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager
{
    public UIManager(GameFace face) : base(face) { }

    private Dictionary<PanelType,BasePanel> panelDict = new Dictionary<PanelType,BasePanel>();
    private Dictionary<PanelType, string> panelPath = new Dictionary<PanelType, string>();
    private Stack<BasePanel> panelStack = new Stack<BasePanel>();

    private Transform canvasTransform;
    private MessagePanel messagePanel;

    public override void OnInit()
    {
        base.OnInit();
        InitPanel();
        canvasTransform = GameObject.Find("Canvas").GetComponent<Transform>();

        PushPanel(PanelType.Message);
        PushPanel(PanelType.Start);

    }

    ///<summary>
    ///��UI��ʾ�ڽ�����
    ///</summary>
    ///<param name="panelType"></param>
    public BasePanel PushPanel(PanelType panelType)
    {
        if(panelDict.TryGetValue(panelType,out BasePanel panel))//����Ƿ��ѱ�ʵ����
        {
            if(panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }
            panelStack.Push(panel);
            //Debug.Log("push���Ѵ��ڶ���" + panel);
            panel.OnEnter();
            return panel;
        }
        else
        {
            BasePanel panel0 = SpawnPanel(panelType);
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }
            panelStack.Push(panel0);
            //Debug.Log("push�Ĳ����ڶ���" + panel0);
            panel0.OnEnter();
            return panel0;
        }
    }

    public void PopPanel()
    {
        if (panelStack.Count == 0) return;
        BasePanel topPanel = panelStack.Pop();
        
        //Debug.Log("pop�Ķ���:"+topPanel);
        topPanel.OnExit();

        //if (panelStack.Count == 0) return;
        BasePanel panel = panelStack.Peek();
        panel.OnRecovery();
    }

    ///<summary>
    ///���ɶ�Ӧ��UI
    ///</summary>
    ///<param name="panelType"></param>
    private BasePanel SpawnPanel(PanelType panelType)
    {
        if(panelPath.TryGetValue(panelType,out string path))
        {
            GameObject g = GameObject.Instantiate(Resources.Load(path)) as GameObject;//��·���ҵ�Ԥ���岢ʵ����
            g.transform.SetParent(canvasTransform,false);
            BasePanel panel = g.GetComponent<BasePanel>();
            panel.SetUIManager = this;//��ʼ��UIManagerʵ��
            panelDict.Add(panelType, panel);
            return panel;
        }
        else
        {
            return null; 
        }
    }
    ///<summary>
    ///ʵ����UI·��
    ///</summary>
    private void InitPanel()
    {
        string panelpath = "Panel/";
        string[] path = new string[] 
        { 
            "MessagePanel",
            "StartPanel",
            "LoginPanel",
            "LogonPanel",
            "RoomListPanel",
            "RoomPanel",
            "GamePanel"
        };
        panelPath.Add(PanelType.Message, panelpath + path[0]);
        panelPath.Add(PanelType.Start, panelpath + path[1]);
        panelPath.Add(PanelType.Login, panelpath + path[2]);
        panelPath.Add(PanelType.Logon, panelpath + path[3]);
        panelPath.Add(PanelType.RoomList, panelpath + path[4]);
        panelPath.Add(PanelType.Room, panelpath + path[5]);
        panelPath.Add(PanelType.Game, panelpath + path[6]);
    }
    public void SetMessagePanel(MessagePanel message)
    {
        messagePanel = message;//����
    }
    public void ShowMessage(string str,bool sync = false)
    {
        messagePanel.ShowMessage(str,sync);
    }
}
