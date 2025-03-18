using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;
using Unity.VisualScripting;

public class PlayerManager : BaseManager
{
    public PlayerManager(GameFace face) : base(face) { }
    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();//按照加入顺序设置ID 或 按照用户名设置ID
    private GameObject character;
    private GameObject bullet;
    private Transform spawnPos;

    public override void OnInit()
    {
        base.OnInit();
        character = Resources.Load("Character/Player") as GameObject;
        bullet = Resources.Load("Character/Bullet") as GameObject;
    }
    public void AddPlayer(MainPack pack)
    {
        spawnPos = GameObject.Find("SpawnPos").transform;
        foreach (var p in pack.Playerpack)
        {
            GameObject g = GameObject.Instantiate(character, spawnPos.position, Quaternion.identity);
            if(p.Playername.Equals(face.UserName))
            {
                //创建当前客户端角色
                g.transform.gameObject.AddComponent<UpdatePosRequest>();
                g.transform.gameObject.AddComponent<UpdatePos>();
                g.transform.gameObject.AddComponent<CharacterController>();
                g.transform.Find("Rotate").gameObject.AddComponent<FireRequest>();
                g.transform.Find("Rotate").gameObject.AddComponent<GunController>();
            }
            else
            {
                //创建其他客户端的角色
                GameObject.Destroy(g.GetComponent<Rigidbody>());
            }
            //添加角色
            players.Add(p.Playername,g);
        }
    }
    public void RemovePlayer(string id)
    {
        if (players.TryGetValue(id,out GameObject g))
        {
            GameObject.Destroy(g);
            players.Remove(id);
        }
        else
        {
            Debug.Log("移除角色错误");
        }

    }
    public void GameExit()
    {
        foreach (var value in players.Values)
        {
            GameObject.Destroy(value);
        }
        players.Clear();
    }

    public void UpdatePos(MainPack pack)
    {
        PosPack posPack = pack.Playerpack[0].Pospack;
        if (players.TryGetValue(pack.Playerpack[0].Playername,out GameObject g))
        {
            Vector3 pos = new Vector3(posPack.PosX,posPack.PosY,0);//设置位置
            float rot = posPack.PosZ;
            float gunrotX = posPack.GunRotX;
            float gunrotY = posPack.GunRotY;

            g.transform.position = pos;
            g.transform.eulerAngles = new Vector3(0,0,rot);
            g.transform.Find("Rotate").eulerAngles = new Vector3(gunrotX, gunrotY, 0);
        }
    }
    public void SpawnBullet(MainPack pack)
    {
        Vector3 pos = new Vector3(pack.Bulletpack.PosX,pack.Bulletpack.PosY,0);
        float rot = pack.Bulletpack.RosZ;
        Vector3 mpos = new Vector3(pack.Bulletpack.MPosX,pack.Bulletpack.MPosY,0);
        Vector3 velocity = (mpos - pos).normalized * 20;
        GameObject g = GameObject.Instantiate(bullet,pos,Quaternion.identity);
        g.transform.eulerAngles = new Vector3(0, 0, rot);
        g.GetComponent<Rigidbody>().velocity = velocity;
    }
}
