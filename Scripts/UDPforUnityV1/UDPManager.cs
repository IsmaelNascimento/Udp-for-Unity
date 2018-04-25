using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public abstract class UDPManager : MonoBehaviour
{
    #region Variables

    public static UDPManager Instance;
    private UdpClient udpClient;
    private IPEndPoint endPoint;
    [SerializeField] private int portClient = 8430;

    #endregion

    #region Methods MonoBehaviour

    private void Awake()
    {
        Instance = this;
    }

    public virtual void Start()
    {
        udpClient = new UdpClient(portClient);
        print("Client listening for messages in port " + portClient);
        StartReceivingMessages();
    }

    #endregion

    #region Methods for UDP

    public void StartReceivingMessages()
    {
        udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }

    public void ReceiveCallback(IAsyncResult res)
    {
        var dataReceived = udpClient.EndReceive(res, ref endPoint);
        MessageReceived(dataReceived, endPoint.Address.ToString(), endPoint.Port);
        udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
    }

    public abstract void MessageReceived(byte[] data, string ipHost, int portHost);

    public void SendDataUDP(string data, string ipHost, int portHost)
    {
        if(udpClient == null)
            udpClient = new UdpClient(portClient);

        var endPoint = new IPEndPoint(IPAddress.Parse(ipHost), portHost);
        var dataInBytes = data.StringToByteArray();
        udpClient.Send(dataInBytes, dataInBytes.Length, endPoint);
        print(string.Format("Send data:: {0} for IP:: {1} and Port:: {2}", data, ipHost, portHost));
    }

    #endregion
}