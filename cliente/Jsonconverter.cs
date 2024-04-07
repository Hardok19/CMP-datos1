using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using loggings;
using Newtonsoft.Json.Linq;


    namespace Json
    {
        public class Song{
        public string SongName { get; set; }
        public string Artist { get; set; }
        public string Id { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        }

        public class JsonToQueue{
        private static readonly ILogger<JsonToQueue> _logger = Logger.CreateLogger<JsonToQueue>();

        public Queue<Song> ToQueue(string jsonString){
            _logger.LogDebug(jsonString);
            try{
                var songs = JsonConvert.DeserializeObject<List<Song>>(jsonString);
                var queue = new Queue<Song>(songs);
                return queue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al convertir JSON a cola: {ex.Message}");
                return null;
            }
        }     
    }

}






