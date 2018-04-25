using UnityEngine;

public class UDPController : UDPManager
{
    public string ipHost = "255.255.255.255";
    public int portHost = 0000;
    public string messageForHost = "";

    public override void Start ()
    {
        base.Start();
    }

    public override void MessageReceived(byte[] data, string ipHost, int portHost)
    {
        print(string.Format("Received data:: {0} of IP:: {1} and Port:: {2}", data.ByteArrayToString(), ipHost, portHost));
    }

    [ContextMenu("SendMyData")]
    public void SendMyData()
    {
        SendDataUDP(messageForHost, ipHost, portHost);
    }
}
