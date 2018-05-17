using System.Net;
using System.Net.Sockets;

/// <summary>
/// This class is responsible for reading UDP datagrams.
/// </summary>
public class UDP_Listener
{
    private int UDP_PORT;
    private IPAddress _IPAddress;
    private UdpClient udpClient;

    /// <summary>
    /// Creates an instance of UDP_Listener.
    /// </summary>
    /// <param name="UDP_PORT">The port to search for UDP datagrams.</param>
    /// <param name="_IPAddress">The IP address to search for UDP datagrams.</param>
    public UDP_Listener(int UDP_PORT, string _IPAddress)
    {
        //  Save the configuration parameters.
        this.UDP_PORT = UDP_PORT;
        this._IPAddress = IPAddress.Parse(_IPAddress);

        //  Create the UDP Client.
        udpClient = UDP_Protocol.CreateUDPClient(UDP_PORT);
    }

    /// <summary>
    /// Reads data from a UDP socket.
    /// </summary>
    /// <returns></returns>
    public string ReceiveData()
    {
        return UDP_Protocol.ReadData(udpClient);
    }

    /// <summary>
    /// Closes the UDP socket.
    /// </summary>
    public void Close()
    {
        //  Close the UDP connection.
        UDP_Protocol.CloseConnection(udpClient);

        //  Free up the resources.
        _IPAddress = null;
        udpClient = null;
    }
}
