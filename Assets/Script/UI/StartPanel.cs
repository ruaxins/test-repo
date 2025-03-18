using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button startBtn;
    private void Start()
    {
        startBtn.onClick.AddListener(StartButtonOnClick);
    }
    private void StartButtonOnClick()
    {
        uiManager.PushPanel(PanelType.Login);
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
