using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rig;
    private bool island;

    private float jumpspeed = 15;
    private float movespeed = 6;

    private void Start()
    {
        Physics.gravity = new Vector3(0, -20f, 0);//修改全局重力
        rig = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Move();
        Jump();
    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        if(h!=0)
        {
            rig.velocity = new Vector3(h * movespeed, rig.velocity.y,rig.velocity.z);
        }
    }
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && island)
        {
            rig.velocity = new Vector3(rig.velocity.x, jumpspeed, rig.velocity.z);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Land") island = true; 
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Land") island = false;
    }
}
