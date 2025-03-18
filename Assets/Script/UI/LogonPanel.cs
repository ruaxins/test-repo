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
            Debug.Log("用户名与密码不能为空！");
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
                Debug.LogWarning("未注册");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("注册成功");
                uiManager.PushPanel(PanelType.Login);
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("注册失败");
                break;
            default:
                Debug.LogWarning("注册意外");
                break;
        }
    }
}
