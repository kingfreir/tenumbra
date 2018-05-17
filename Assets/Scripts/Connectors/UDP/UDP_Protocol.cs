using System.Net;
using System.Net.Sockets;
using System.Text;

/// <summary>
/// This class contains the methods necessary for the UDP Protocol
/// </summary>
public sealed class UDP_Protocol
{
    /// <summary>
    /// Creates a new UDP Client object.
    /// </summary>
    /// <param name="Port">Port that the UDP client will receive/send datagrams.</param>
    /// <returns></returns>
    public static UdpClient CreateUDPClient(int Port)
    {
        return new UdpClient(Port);
    }

    /// <summary>
    /// Closes the UDP connection.
    /// </summary>
    /// <param name="udpClient">The UdpClient object that will have the connection closed.</param>
    public static void CloseConnection(UdpClient udpClient)
    {
        udpClient.Close();
    }

    /// <summary>
    /// Reads the data sent from any host on a previously defined UDP Port. Please note that this is a blocking operation!
    /// </summary>
    /// <param name="udpClient"> The UdpClient from which the datagram will be read from.</param>
    /// <returns>A string that contains the encoded data sent by the host.</returns>
    public static string ReadData(UdpClient udpClient)
    {
        //  Receive data from any source.
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(0, 0);

        //  Read an UDP Datagram.   NOTE: This is a blocking operation!
        byte[] receivedBytes = udpClient.Receive(ref RemoteIpEndPoint);

        //  Encode the bytes received into a string.
        string data = Encoding.ASCII.GetString(receivedBytes);

        return data;
    }
}
