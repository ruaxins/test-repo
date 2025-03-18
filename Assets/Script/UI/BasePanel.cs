using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected UIManager uiManager;
    protected GameFace face
    {
        get
        {
            return GameFace.Face;
        }
    }
    public UIManager SetUIManager {  set { uiManager = value; } }
    public virtual void OnEnter()
    {

    }
    public virtual void OnPause()
    {

    }
    public virtual void OnRecovery()//»Ö¸´
    {

    }
    public virtual void OnExit()
    {

    }
}
