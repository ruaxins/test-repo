using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Button join;
    public Text title, num, status;

    public RoomListPanel roomListPanel;
    void Start()
    {
        join.onClick.AddListener(OnJoinClick);
    }

    private void OnJoinClick()
    {
        roomListPanel.JoinRoom(title.text);
    }

    public void SetRoomItemInfo(string title, int currentnum, int maxnum, int status)
    {
        this.title.text = title;
        this.num.text = currentnum + "/" + maxnum;
        switch (status)
        {
            case 0:
                this.status.text = "�ȴ�����";break;
            case 1:
                this.status.text = "��������";break ;
            case 2:
                this.status.text = "��Ϸ��";break;
            default:
                Debug.Log("����״̬����");break;
        }
    }
}
