using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

static async Task Main(string[] args) {
    TcpListener server = new TcpListener(IPAddress.Any, 6379);
    server.Start();

    while (true)
    {
        Socket client = await server.AcceptSocketAsync();
        Thread clientThread = new Thread(() => HandleClient(client));
        clientThread.Start();
    }
}

static void HandleClient(Socket client) {
    using (client)
    using (NetworkStream ns = new NetworkStream(client)) {
        byte[] buffer = new byte[1024];
        int bytesRead = client.Receive(buffer);
        while ((bytesRead = ns.Read(buffer, 0 , buffer.Length)) != 0) {
            if (bytesRead > 0) {
                client.Send(Encoding.UTF8.GetBytes("+PONG\r\n"));
            }
            else break;
        }
    }
}