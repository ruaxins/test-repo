using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogonPanel : BasePanel
{
    public LogonRequest logonRequest;
    public InputField user, pass;
    public Button logonBun,switchBtn; 

    private void Start()
    {
        logonBun.onClick.AddListener(OnLogonClick);
        switchBtn.onClick.AddListener(SwitchLogin);
    }
    private void OnLogonClick()
    {
        if (user.text == "" || pass.text == "")
        {
            Debug.Log("�û��������벻��Ϊ�գ�");
            return;
        }   
        logonRequest.SendRequest(user.text,pass.text);
    }

    private void SwitchLogin()
    {
        Debug.Log("LoginPop");
        uiManager.PopPanel();
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

    public void OnResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.ReturnNone:
                Debug.LogWarning("δע��");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("ע��ɹ�");
                uiManager.PushPanel(PanelType.Login);
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("ע��ʧ��");
                break;
            default:
                Debug.LogWarning("ע������");
                break;
        }
    }
}
