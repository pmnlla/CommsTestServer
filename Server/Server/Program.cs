using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener?view=net-5.0
            TcpListener server = null;
            try
            {
                Int32 port = 12346;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                // configure server and start it up
                server = new TcpListener(localAddr, port);
                server.Start();

                // data buffer
                Byte[] bytes = new Byte[1024];
                string data; 

                // wait for connection fron client 
                while(true)
                {
                    Console.WriteLine("waiting...");
                    TcpClient tempCLient = server.AcceptTcpClient();
                    Console.WriteLine("Connected");

                    data = null;
                    NetworkStream inputstream = tempCLient.GetStream();

                    int i;
                    while((i = inputstream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // process data sent by client process through translating data bytes to ascii string and making it lowercase
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        data = data.ToLower();
                        Console.WriteLine("Recieved: {0}", data);

                        byte[] messagetosend = System.Text.Encoding.ASCII.GetBytes(data);

                        // send response to client
                        inputstream.Write(messagetosend, 0, messagetosend.Length);
                    }

                    tempCLient.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(" error occured: " + e.ToString());
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
