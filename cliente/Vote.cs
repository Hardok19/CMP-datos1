using Sockets;
using loggings;
using Microsoft.Extensions.Logging;
using Json;

namespace votes{
    public class Vote{
        private readonly ILogger<Vote> _logger = Logger.CreateLogger<Vote>();

        public void Down(string song, ClientSocket clientSock){
            JSONGenerator json = new JSONGenerator();
            Dictionary<string, object> data = new Dictionary<string, object>{
                {"command", "down.vote"},
                {"id", song}
                
            };
            string voto = json.GenerateJSON(data);
            
            clientSock.ProcessData(voto);
            _logger.LogInformation($"Voting down for song: {song}");
        }

        public void Up(string song, ClientSocket clientSock){
            JSONGenerator json = new JSONGenerator();
            Dictionary<string, object> data = new Dictionary<string, object>{
                {"command", "up.vote"},
                {"id", song}
                
            };
            string voto = json.GenerateJSON(data);

            clientSock.ProcessData(voto);
            
            _logger.LogInformation($"Voting up for song: {song}");
            
        }
    }
}