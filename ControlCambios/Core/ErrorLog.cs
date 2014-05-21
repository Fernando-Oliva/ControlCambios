using System;
using System.IO;

namespace ControlCambios.Core
{
    public class ErrorLog
    {
        string fileName = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";

        public void WriteLog(string errorMessage)
        {
            File.AppendAllText(fileName, errorMessage + Environment.NewLine + Environment.NewLine);
        }
    }
}
