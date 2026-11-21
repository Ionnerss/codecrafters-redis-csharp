using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();

while (true) {
    TcpClient client = server.AcceptTcpClient();
    NetworkStream ns = client.GetStream();
    if (client.ReceiveBufferSize > 0)
    {
        client.Send(Encoding.UTF8.GetBytes("+PONG\r\n"));
    }
}


