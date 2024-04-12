using System.Text.Json;
using loggings;
using Microsoft.Extensions.Logging;
namespace Json{

    public class JSONGenerator
    {
        private static readonly ILogger<JSONGenerator> _logger = Logger.CreateLogger<JSONGenerator>();
        public string GenerateJSON(Dictionary<string, object> data)
        {
            try{
                return JsonSerializer.Serialize(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al generar JSON: {ex.Message}");
                return null;
            }
        }
    }
}