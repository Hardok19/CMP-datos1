using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using loggings;

namespace JsonM
{
    public class Json{
        private static readonly ILogger<Json> _logger = Logger.CreateLogger<Json>();

        public static Queue<string> Convert(string json){
            try{

                // Deserializar el JSON a una lista de string
                List<string> songNames = JsonConvert.DeserializeObject<List<string>>(json);

                // Convertir la lista de canciones a una cola
                Queue<string> songQueue = new Queue<string>(songNames);


                return songQueue;
            }
            catch (JsonException ex){
                _logger.LogError(ex, "Error al deserializar JSON: {message}", ex.Message);
                return null; 
            }
        }
    }
}






