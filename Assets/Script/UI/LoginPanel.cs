using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public LoginRequest loginRequest;
    public InputField user, pass;
    public Button loginBun, switchBtn;

    private void Start()
    {
        loginBun.onClick.AddListener(OnLogonClick);
        switchBtn.onClick.AddListener(SwitchLogon);
    }
    private void OnLogonClick()
    {
        if (user.text == "" || pass.text == "")
        {
            Debug.Log("用户名与密码不能为空！");
            return;
        }
        loginRequest.SendRequest(user.text, pass.text);
    }

    private void SwitchLogon()
    {
        uiManager.PushPanel(PanelType.Logon);
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
                Debug.LogWarning("未登录");
                break;
            case ReturnCode.Succeed:
                uiManager.ShowMessage("登录成功");
                face.UserName = user.text;
                uiManager.PushPanel(PanelType.RoomList);
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessage("登录失败");
                break;
            default:
                Debug.LogWarning("登录意外");
                break;
        }
    }
}
