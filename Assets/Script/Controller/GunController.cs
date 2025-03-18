using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private float bulletspeed = 20;
    private float deep = 10;
    private GameObject bullet;
    private Transform firePos;
    private FireRequest fireRequest;
    private void Start()
    {
        fireRequest = GetComponent<FireRequest>();
        bullet = Resources.Load("Character/Bullet") as GameObject;
        firePos = transform.Find("Fire");
    }
    private void Update()
    {
        LookAt();
        Fire();
    }
    private void LookAt()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = deep; // 距离摄像机的深度
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.LookAt(worldPos);
    }
    private void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = deep; // 距离摄像机的深度
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            float m_fireAngle = Vector3.Angle(worldPos-transform.position, Vector3.up);
            if(worldPos.x>transform.position.x)
            {
                m_fireAngle = -m_fireAngle;
            }
            GameObject g = Instantiate(bullet,firePos.position,Quaternion.identity);
            g.transform.eulerAngles = new Vector3(0,0,m_fireAngle);//设置子弹朝向
            Vector3 velocity = (worldPos - firePos.position).normalized * bulletspeed;
            g.GetComponent<Rigidbody>().velocity = velocity;
            fireRequest.SendRequest(firePos.position,m_fireAngle,worldPos);
        }

    }
}
