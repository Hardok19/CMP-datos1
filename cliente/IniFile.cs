using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IniA
{
    class IniFile
    {
        string iniFilePath;
        string exeName = AppDomain.CurrentDomain.FriendlyName;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern uint WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public IniFile(string iniPath = null)
        {
            iniFilePath = Path.Combine(Directory.GetCurrentDirectory(), iniPath ?? exeName + ".ini");
        }

        public string Read(string key, string section = null)
        {
            var retVal = new StringBuilder(255);
            GetPrivateProfileString(section ?? exeName, key, "", retVal, 255, iniFilePath);
            return retVal.ToString();
        }

        public void Write(string key, string value, string section = null)
        {
            WritePrivateProfileString(section ?? exeName, key, value, iniFilePath);
        }

        public void DeleteKey(string key, string section = null)
        {
            Write(key, null, section ?? exeName);
        }

        public void DeleteSection(string section = null)
        {
            Write(null, null, section ?? exeName);
        }

        public bool KeyExists(string key, string section = null)
        {
            return Read(key, section).Length > 0;
        }
    }
}
