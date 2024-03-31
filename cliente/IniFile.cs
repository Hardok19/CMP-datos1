using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using loggings;
using Microsoft.Extensions.Logging;

namespace IniA
{
    class IniFile
    {
        string iniFilePath;
        string exeName = AppDomain.CurrentDomain.FriendlyName;
        private static readonly ILogger<IniFile> _logger = Logger.CreateLogger<IniFile>();

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
            if (!string.IsNullOrEmpty(retVal.ToString())){
                return retVal.ToString();

            }
            else{
                _logger.LogCritical("Error occurred: La ubicación que se intenta leer en config está vacía");
                return "";
                
            }
        }

        public void Write(string key, string value, string section = null){
        
            try{
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
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


        public void DeleteKey(string key, string section = null){
            try{
                // Se intenta eliminar la clave pasando el valor como nulo
                Write(key, null, section ?? exeName);
            }
            catch (Exception ex){
                _logger.LogError($"Error al intentar eliminar la clave '{key}' en la sección '{section ?? exeName}': {ex.Message}");
            }
        }


        public void DeleteSection(string section = null){
            try{
                // Se intenta eliminar la sección pasando la clave y el valor como nulos
                Write(null, null, section ?? exeName);
            }
            catch (Exception ex){
                _logger.LogError($"Error al intentar eliminar la sección '{section ?? exeName}': {ex.Message}");
            }
        }


        public bool KeyExists(string key, string section = null){
            try{
                string value = Read(key, section);
                return !string.IsNullOrEmpty(value);
            }
            catch (Exception ex){
                _logger.LogError($"Error al verificar la existencia de la clave '{key}' en la sección '{section ?? exeName}': {ex.Message}");
                // En caso de error, se asume que la clave no existe
                return false;
            }
        }
    }
}
