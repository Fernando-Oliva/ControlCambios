using Microsoft.Win32;
using System.IO;
using System;

namespace ControlCambios.Core
{
    public static class FilesCore
    {
        public static string SelectFile()
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.txt";

            if (!File.Exists(filePath))
            {
                File.AppendAllText(filePath, "C:\\");
            }

            string[] drive = File.ReadAllLines(filePath);            

            ofdSelectFile.InitialDirectory = drive[0];
            ofdSelectFile.ShowDialog();

            return ofdSelectFile.FileName;
        }
    }
}
