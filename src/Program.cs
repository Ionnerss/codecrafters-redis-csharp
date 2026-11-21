using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class ThreadedServer {
    static void Main() {
        TcpListener server = new TcpListener(IPAddress.Any, 6379);
        server.Start();
        
        while (true) {
            Socket client = server.AcceptSocket();
            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.IsBackground = true;
            clientThread.Start();
        }
    }
    static void HandleClient(Socket client) {
        using (client)
        using (NetworkStream ns = new NetworkStream(client)) {
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = ns.Read(buffer, 0 , buffer.Length)) != 0) {
                if (bytesRead > 0) {
                    client.Send(Encoding.UTF8.GetBytes("+PONG\r\n"));
                }
                else break;
            }
        }
    }
}

