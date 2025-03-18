using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpdatePos : UpdatePosRequest
{
    public UpdatePosRequest posRequest;
    private Transform gunTransform;
    void Start()
    {
        posRequest = transform.GetComponent<UpdatePosRequest>();
        gunTransform = transform.Find("Rotate");
        InvokeRepeating("UpdatePosFun",1f,1f/60f);//在脚本启动1秒后开始，以60FPS的频率调用
    }
    private void UpdatePosFun()
    {
        Vector3 pos = transform.position;
        float characterRot = transform.eulerAngles.y;
        float gunRotx = gunTransform.eulerAngles.x;
        float gunRoty = gunTransform.eulerAngles.y;
        posRequest.SendRequest(pos,characterRot,gunRotx,gunRoty);
    }
}
