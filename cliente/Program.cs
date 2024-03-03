using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Sockets
{
    class ClientSocket
    {
        private int port;
        private TcpClient client;
        private NetworkStream stream;

        public ClientSocket(int port, int sendTimeout, int receiveTimeout)
        {
            this.port = port;
            client = new TcpClient("localhost", port);

            client.ReceiveTimeout = sendTimeout;
            client.SendTimeout = receiveTimeout;
            stream = client.GetStream();
        }

        public void processData(String data)
        {
            byte[] buf;

            /* append newline as server expects a line to be read */
            buf = Encoding.UTF8.GetBytes(data+"\n");

            Console.WriteLine("Sending data \'{0}\' to server", data);
            
            stream.Write(buf, 0, data.Length + 1);

            buf = new byte[100];
            int bytesRead = stream.Read(buf, 0, 100);

            byte[] finalData = new byte[bytesRead];

            for(int i=0; i < bytesRead; i++)
            {
                finalData[i] = buf[i];
            }

            string response = Encoding.UTF8.GetString(finalData);

            response = response.TrimEnd();

            Console.WriteLine("Received Response : \'{0}\', of length {1}", response, response.Length);

            client.Close();
        }

    }

    class Client
    {
        static void Main(string[] args)
        {
            try
            {
                ClientSocket clientSock = new ClientSocket(1234, 3000, 3000);
                String data = "Hello World";

                clientSock.processData(data);

            
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press Enter to close the connection");

            Console.Read();
        }
    }
}
