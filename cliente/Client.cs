using System.Text;
using System.Net.Sockets;
using IniA;
using Newtonsoft.Json;
using votes;
using loggings;
using Microsoft.Extensions.Logging;
using GUI_CLIENTE;
using Json;
using Newtonsoft.Json.Linq;



namespace Sockets{

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

        public string ProcessData(string data){

            try
            {

                byte[] buf = Encoding.UTF8.GetBytes(data + "\n");


                //_logger.LogInformation("Enviando data '{0}' al server", data);
                
                stream.Write(buf, 0, buf.Length);

                //_logger.LogInformation("Data enviada correctamente.");

                buf = new byte[800];
                int bytesRead = stream.Read(buf, 0, 800);
                string response = Encoding.UTF8.GetString(buf, 0, bytesRead).Trim();

                //_logger.LogInformation("Recibida respuesta del server : '{0}'", response);
                return response;
            
            }
            catch (IOException ex) {
                _logger.LogError("Error de E/S: " + ex.Message);
                return "";
            }               
        }

         public void AddQueueToColumns(Queue<Song> queue, ListView listViewSongs){
            // Limpiar la lista de vista antes de agregar nuevos elementos
            listViewSongs.Items.Clear();

            // Recorrer la cola y agregar cada elemento a la lista de vista
            foreach (var song in queue)
            {
                var item = new ListViewItem(song.SongName);
                item.SubItems.Add(song.Artist);
                item.SubItems.Add(song.Likes.ToString());
                item.SubItems.Add(song.Dislikes.ToString());
                listViewSongs.Items.Add(item);
            }
        }

    }

    
}
