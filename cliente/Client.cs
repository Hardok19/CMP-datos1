using System.Text;
using System.Net.Sockets;
using IniA;
using System.Text.Json.Nodes;
using JsonM;
using Newtonsoft.Json;



namespace Sockets
{
    public class Poll{
        
        public static async Task Request(ClientSocket clientSock, CancellationToken cancellationToken){
            int delay;
            var config = new IniFile("config.ini"); //Variable de ruta archivo configuracion
            delay = Convert.ToInt32(config.Read("Delay"));

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // Enviar la solicitud al servidor usando el método ProcessData
                    clientSock.ProcessData("GetPlaylistUpdates");

                    // Esperar un tiempo antes de enviar la próxima solicitud
                    await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken); // Espera de 5 segundos
                }
            }
            catch (OperationCanceledException)
            {
                // La operación fue cancelada, probablemente porque se solicitó la cancelación
                Console.WriteLine("Polling operation cancelled.");
            }
        }
    }  

    public class ClientSocket
    {
        private int port;
        private TcpClient client;
        private NetworkStream stream;



        public ClientSocket(string Address, int port, int sendTimeout, int receiveTimeout)
        {
            this.port = port;
            client = new TcpClient(Address, port);

            client.ReceiveTimeout = sendTimeout;
            client.SendTimeout = receiveTimeout;
            stream = client.GetStream();
        }

        public void ProcessData(string data){

            try
            {

                byte[] buf = Encoding.UTF8.GetBytes(data + "\n");

                Console.WriteLine("Sending data '{0}' to server", data);
                
                stream.Write(buf, 0, buf.Length);

                buf = new byte[100];
                int bytesRead = stream.Read(buf, 0, 100);
                string a1 = "up.vote";
                string response = Encoding.UTF8.GetString(buf, 0, bytesRead).Trim();
                //Console.WriteLine("Received Response: '{0}', of length {1}", response, response.Length);
                if(response.StartsWith("Received: ") && response.Substring(10).Equals(a1)) {
                    Console.WriteLine("Received Response: '{0}', of length {1}", response, response.Length);
                }
                    
                if (response.StartsWith("[")){
                    try{
                    Queue<string> result = JsonM.Json.Convert(response);
                        while (result.Count > 0) {
                            Console.WriteLine(result.Dequeue());
                        }
                    }
                    catch(IOException ex){
                        Console.WriteLine("Error reading from network stream: " + ex.Message);
                    }
                }


            }
            catch (IOException ex) {
                Console.WriteLine("Error de E/S: " + ex.Message);
            }               
            catch (JsonException ex) {
                    Console.WriteLine("Error de JSON: " + ex.Message);
            }               
            catch (Exception ex) {
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
            }
        }
        public async Task SendAsync(string message){
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }



        public async Task<string> ReceiveAsync(CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[100];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
        }
    }

    class Client
{
    public static ClientSocket clientSock;

    static async Task Main(string[] args)
    {
        try
        {
            string Address;
            int port;
            var config = new IniFile("config.ini"); //Variable de ruta archivo configuracion
            Address = config.Read("Address");
            port = Convert.ToInt32(config.Read("Port"));

            bool connected = false;

            while (!connected)
            {
                try
                {
                    clientSock = new ClientSocket(Address, port, 3000, 3000);
                    connected = true;
                }
                catch (Exception)
                {
                    // Si no se pudo conectar, esperar un tiempo antes de intentar de nuevo
                    Console.WriteLine("No se pudo conectar. Reintentando en 5 segundos...");
                    await Task.Delay(5000); // Esperar 5 segundos antes de reintentar
                }
            }

            Console.WriteLine("Conexión establecida correctamente");

            string data = "Hello World";
            string data0 = "up.vote";
            string data1 = "GetPlaylistUpdates";

            // Crear un CancellationTokenSource para poder cancelar el polling si es necesario
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            // Iniciar el polling en un hilo aparte
            Task pollingTask = Poll.Request(clientSock, cancellationTokenSource.Token);

            // Esperar hasta que se presione Enter para salir
            Console.WriteLine("Press Enter to close the connection");
            Console.ReadLine();

            // Si se presiona Enter, cancelar el polling
            cancellationTokenSource.Cancel();

            // Esperar a que el polling termine
            await pollingTask;

            Console.WriteLine("Press Enter to close the connection");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
}
