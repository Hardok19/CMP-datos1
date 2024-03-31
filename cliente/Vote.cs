using Sockets;
using loggings;
using Microsoft.Extensions.Logging;

namespace votes{
    public class Vote{
        private readonly ILogger<Vote> _logger;

        public Vote()
        {
            _logger = Logger.CreateLogger<Vote>();
        }

        public void Down(string song, ClientSocket clientsock)
        {
            _logger.LogInformation("Voting down for song: {song}", song);
            clientsock.ProcessData("down.vote" + song);
        }

        public void Up(string song, ClientSocket clientSock)
        {
            _logger.LogInformation("Voting up for song: {song}", song);
            clientSock.ProcessData("up.vote" + song);
        }
    }
}