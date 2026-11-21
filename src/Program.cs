using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();
server.AcceptSocket(); // wait for client

Socket client = server.AcceptSocket();
client.Send(Encoding.UTF8.GetBytes("\\+PONG\r\n"));