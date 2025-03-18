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

    public byte[] Buffer { get { return buffer; } }//缓冲区

    public int StartIndex { get { return startIndex; } }//缓冲区的末尾位置

    public int Remsize { get { return buffer.Length - startIndex; } }//剩余内存

    public void ReadBuffer(int len, Action<MainPack> HandleResponse)
    {
        startIndex += len;//更新缓冲区的末尾位置
        if (startIndex < 4) return;//头部占4个字节
        int count = BitConverter.ToInt32(buffer, 0); //将 buffer 缓冲区前 4 字节转换为一个整数 count(表示消息的内容长度，不包含头部)
        while (true)
        {
            if (startIndex >= (count + 4))//完整的数据包长度是 count + 4 字节
            {
                //buffer 第 4 字节（跳过消息头部）开始，读取长度为 count 的数据，并解析为 MainPack 对象
                MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                HandleResponse(pack);
                Array.Copy(buffer, count + 4, buffer, 0, startIndex - count - 4);//将已传输的数据部分用后面的数据覆盖
                startIndex -= (count + 4);
            }
            else
            {
                break;//完成传输，跳出循环
            }
        }
    }
    public static byte[] PackData(MainPack pack)
    {
        byte[] data = pack.ToByteArray();//包体
        byte[] head = BitConverter.GetBytes(data.Length);//包头
        return head.Concat(data).ToArray();//连接数据包
    }
    public static byte[] PackDataUDP(MainPack pack)
    {
        return pack.ToByteArray();
    }
}
