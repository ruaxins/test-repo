using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    public Text text;
    string msg = null;

    public override void OnEnter()
    {
        base.OnEnter();
        text.CrossFadeAlpha(0,0.1f,false);//������ʾ��
        uiManager.SetMessagePanel(this);
    }
    private void Update()
    {
        if(msg != null)
        {
            ShowText(msg);
            msg = null;
        }
    }
    public void ShowMessage(string str,bool sync = false)
    {
        if (sync)
        {
            //�첽��ʾ
            msg = str;
        }
        else
        {
            ShowText(str);
        }
    }
    private void ShowText(string str)
    {
        text.text = str;
        text.CrossFadeAlpha(1, 0.1f, false);//��ʾ��ʾ��
        Invoke("HideText", 1);
    }
    private void HideText()
    {
        text.CrossFadeAlpha(0, 1f, false);//������ʾ��
    }
}
