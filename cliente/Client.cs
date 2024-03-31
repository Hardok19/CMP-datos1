using System.Text;
using System.Net.Sockets;
using IniA;
using Newtonsoft.Json;
using votes;
using loggings;
using Microsoft.Extensions.Logging;






namespace Sockets
{
    public class Poll{
        private static readonly ILogger<Poll> _logger = Logger.CreateLogger<Poll>();
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
               _logger.LogWarning("Operación de polling cancelada");

            }
        }
    }  

    public class ClientSocket{
        private int port;
        private TcpClient client;
        private NetworkStream stream;
        private static readonly ILogger<ClientSocket> _logger = Logger.CreateLogger<ClientSocket>();

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


                _logger.LogInformation("Enviando data '{0}' al server", data);
                
                stream.Write(buf, 0, buf.Length);

                _logger.LogInformation("Data enviada correctamente.");

                buf = new byte[100];
                int bytesRead = stream.Read(buf, 0, 100);
                string response = Encoding.UTF8.GetString(buf, 0, bytesRead).Trim();

                _logger.LogInformation("Recibida respuesta del server : '{0}'", response);
                
                if (response.StartsWith("[")){
                    _logger.LogInformation("Comenzando conversión de JSON a cola de canciones.");
                    try{
                    Queue<string> result = JsonM.Json.Convert(response);
                        while (result.Count > 0) {
                            _logger.LogInformation(result.Dequeue());
                        }
                        _logger.LogInformation("Conversión de JSON a cola de canciones completada con éxito.");
                    }
                    catch(IOException ex){
                        _logger.LogError("Error reading from network stream: " + ex.Message);
                    }
                }


            }
            catch (IOException ex) {
                _logger.LogError("Error de E/S: " + ex.Message);
            }               
            catch (JsonException ex) {
                _logger.LogError("Error de JSON: " + ex.Message);
            }               
            catch (Exception ex) {
                _logger.LogError("Ocurrió un error: " + ex.Message);
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

    class Client{
    public static ClientSocket clientSock;
    public static Vote votes = new Vote();
    private static readonly ILogger<Client> _logger = Logger.CreateLogger<Client>();


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
                        _logger.LogError("No se pudo conectar. Reintentando en 5 segundos...");
                        await Task.Delay(5000); // Esperar 5 segundos antes de reintentar
                    }
                }

                _logger.LogInformation("Conexión establecida correctamente");



                votes.Up("Cancion 2", clientSock);

                // Crear un CancellationTokenSource para poder cancelar el polling si es necesario
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                // Iniciar el polling en un hilo aparte
                Task pollingTask = Poll.Request(clientSock, cancellationTokenSource.Token);

                // Esperar hasta que se presione Enter para salir
                _logger.LogInformation("Presione Enter para cerrar la conexión");
                Console.ReadLine();

                // Si se presiona Enter, cancelar el polling
                cancellationTokenSource.Cancel();

                // Esperar a que el polling termine
                await pollingTask;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error no controlado: {ErrorMessage}", ex.Message);
            }
        }
    }
}
