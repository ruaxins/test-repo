using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,3f);//三秒后销毁自身
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
