using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;


namespace ControlCambios.Views
{
    /// <summary>
    /// Lógica de interacción para ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.txt";

        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void btnSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(filePath);
            File.AppendAllText(filePath, txtDrive.Text);

            this.Close();
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.ShowDialog();

            txtDrive.Text = folderDialog.SelectedPath;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] drive = File.ReadAllLines(filePath);

            txtDrive.Text = drive[0];
        }
    }
}
