using loggings;
using Microsoft.Extensions.Logging;
using IniA;
using Json;


namespace Sockets{
public class Poll{
        private static readonly ILogger<Poll> _logger = Logger.CreateLogger<Poll>();


        public static async Task Request(ClientSocket clientSock, CancellationToken cancellationToken, ListView listViewSongs, Queue<Song> savequeue, Label labelArtist, Label labelCurrentSong){
            var config = new IniFile("config.ini"); //Variable de ruta archivo configuracion
            int delay = Convert.ToInt32(config.Read("Delay"));
            
            try
            {
                savequeue = null;
                while (!cancellationToken.IsCancellationRequested)
                {
                    // Enviar la solicitud al servidor usando el método ProcessData
                    string response = clientSock.ProcessData("GetPlaylistUpdates");
                    JsonToQueue converter = new JsonToQueue();
                    Queue<Song> queue = converter.ToQueue(response);

                        
                        if (savequeue != null && iguales(queue).Equals(iguales(savequeue))){
                            foreach (var song in savequeue)
                                    {
                                        _logger.LogInformation($"Nombre: {song.SongName}, Artista: {song.Artist}, Likes: {song.Likes}, Dislikes: {song.Dislikes}");
                                    }
                        }
                        else{
 
                            if (queue != null){
                                    AddQueueToColumns(queue, listViewSongs);
                                    current(queue, labelArtist, labelCurrentSong);
                                    savequeue = converter.ToQueue(response);
                                    foreach (var song in savequeue)
                                    {
                                        _logger.LogInformation($"Nombre: {song.SongName}, Artista: {song.Artist}, Likes: {song.Likes}, Dislikes: {song.Dislikes}");
                                    }
                                
                            }
                        }
                        

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
        private static void AddQueueToColumns(Queue<Song> queue, ListView listViewSongs){
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
        private static void current(Queue<Song> queue, Label labelArtist, Label labelCurrentSong){
            foreach(var song in queue){
                labelCurrentSong.Text = song.SongName;
                labelArtist.Text = song.Artist;
                break;
            }
        }


        static string iguales(Queue<Song> queue){
            string result = "";
            foreach (var sng in queue){
                result += sng.SongName.ToString();
                result += sng.Id.ToString();
                result += sng.Likes.ToString();
                result += sng.Dislikes.ToString();

            }
            return result;

        }

    }  
}