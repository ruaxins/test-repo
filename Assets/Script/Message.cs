using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SocketGameProtocol;
using Google.Protobuf;

public class Message
{
    private byte[] buffer = new byte[1024];

    private int startIndex;

    public byte[] Buffer { get { return buffer; } }//������

    public int StartIndex { get { return startIndex; } }//��������ĩβλ��

    public int Remsize { get { return buffer.Length - startIndex; } }//ʣ���ڴ�

    public void ReadBuffer(int len, Action<MainPack> HandleResponse)
    {
        startIndex += len;//���»�������ĩβλ��
        if (startIndex < 4) return;//ͷ��ռ4���ֽ�
        int count = BitConverter.ToInt32(buffer, 0); //�� buffer ������ǰ 4 �ֽ�ת��Ϊһ������ count(��ʾ��Ϣ�����ݳ��ȣ�������ͷ��)
        while (true)
        {
            if (startIndex >= (count + 4))//���������ݰ������� count + 4 �ֽ�
            {
                //buffer �� 4 �ֽڣ�������Ϣͷ������ʼ����ȡ����Ϊ count �����ݣ�������Ϊ MainPack ����
                MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                HandleResponse(pack);
                Array.Copy(buffer, count + 4, buffer, 0, startIndex - count - 4);//���Ѵ�������ݲ����ú�������ݸ���
                startIndex -= (count + 4);
            }
            else
            {
                break;//��ɴ��䣬����ѭ��
            }
        }
    }
    public static byte[] PackData(MainPack pack)
    {
        byte[] data = pack.ToByteArray();//����
        byte[] head = BitConverter.GetBytes(data.Length);//��ͷ
        return head.Concat(data).ToArray();//�������ݰ�
    }
    public static byte[] PackDataUDP(MainPack pack)
    {
        return pack.ToByteArray();
    }
}
