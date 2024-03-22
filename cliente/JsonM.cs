using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonM {
    public class Json {
        public static Queue<string> Convert(string json) {
            try {
                // Deserializar el JSON a una lista de string
                List<string> songNames = JsonConvert.DeserializeObject<List<string>>(json);

                // Convertir la lista de canciones a una cola
                Queue<string> songQueue = new Queue<string>(songNames);

                return songQueue;
            }
            catch (JsonException ex) {
                Console.WriteLine("Error al deserializar JSON: " + ex.Message);
                return null; // o cualquier otro manejo de error apropiado
            }
        }
    }
}





