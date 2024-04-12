using System.Runtime.InteropServices;
using System.Text;
using loggings;
using Microsoft.Extensions.Logging;

namespace IniA
{
    class IniFile{
        // Declaración de variables
        string iniFilePath; // Ruta del archivo .ini
        string exeName = AppDomain.CurrentDomain.FriendlyName; // Nombre del ejecutable
        private static readonly ILogger<IniFile> _logger = Logger.CreateLogger<IniFile>(); // Instancia de logger

        // Declaración de funciones externas para manipulación de archivos .ini (BIBLIOTECA INTERNA DE WINDOWS)
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern uint WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);


        public IniFile(string iniPath = null)
        {
            try
            {
                iniFilePath = Path.Combine(Directory.GetCurrentDirectory(), iniPath ?? exeName + ".ini");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al inicializar el archivo INI: {ex.Message}");
                // En caso de error, se asigna un valor predeterminado para iniFilePath
                iniFilePath = "default.ini";
            }
        }


        public  string Read(string key, string section = null)
        {
            var retVal = new StringBuilder(255);
            GetPrivateProfileString(section ?? exeName, key, "", retVal, 255, iniFilePath);
            if (!string.IsNullOrEmpty(retVal.ToString())){ // ! invierte el valor de verdad  string.IsNullOrEmpty verifica si retval es "" o null
                return retVal.ToString();

            }
            else{
                _logger.LogCritical("Error occurred: La ubicación que se intenta leer en config está vacía");
                return "";
                
            }
        }

        public void Write(string key, string value, string section = null){
        
            try{
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) // string.IsNullOrEmpty verifica si retval es "" o null
                {
                    _logger.LogError("Error: La clave o el valor proporcionado están vacíos.");
                    return;
                }

                WritePrivateProfileString(section ?? exeName, key, value, iniFilePath);
            }
            catch (Exception ex){
            _logger.LogError($"Error al escribir en el archivo de configuración: {ex.Message}");
            }
        }


    }
}
