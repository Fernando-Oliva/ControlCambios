using System;
using System.Windows;
using System.Windows.Media;
using ControlCambios.Core;
using ControlCambios.Clases;
using System.Data;

namespace ControlCambios.Views
{
    /// <summary>
    /// Lógica de interacción para ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        public ChangeWindow()
        {
            InitializeComponent();
        }

        private void UpdateGrid(DBLayer dbConnection)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).dgChanges.DataContext = dbConnection.updateRows();
                }
            }
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            txtFileName.Text = FilesCore.SelectFile();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFileName.Text))
            {
                DBLayer dbConnection = new DBLayer();

                if (dbConnection.InsertData(txtFileName.Text, txtChangeDescription.Text, cbPRE.IsChecked.Value))
                {
                    MessageBox.Show("Registro guardado", "Oiga!", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.Close();

                    UpdateGrid(dbConnection);
                }
                else
                {
                    MessageBox.Show("Error al guardar", "Oiga!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un archivo!","Oiga!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DBLayer connection = new DBLayer();
            DataTable table = new DataTable();

            table = connection.SelectRow(Registro.selectId);

            if (table.Rows.Count > 0)
            {
                txtFileName.Text = table.Rows[0].ItemArray[1].ToString();
                txtChangeDescription.Text = table.Rows[0].ItemArray[2].ToString();

                txtChangeDescription.IsReadOnly = true;
                txtChangeDescription.Background = Brushes.LightGray;

                btnUpdate.Visibility = System.Windows.Visibility.Visible;
                btnSave.Visibility = System.Windows.Visibility.Hidden;

                btnSelectFile.IsEnabled = false;
            }
            else
            {
                txtFileName.Text = string.Empty;
                txtChangeDescription.Text = string.Empty;

                txtChangeDescription.IsReadOnly = false;
                txtChangeDescription.Background = Brushes.White;

                btnSelectFile.IsEnabled = true;
                btnUpdate.Visibility = System.Windows.Visibility.Hidden;
                btnSave.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (cbPRE.IsChecked.Value.Equals(true))
            {
                DBLayer connection = new DBLayer();

                if (connection.UpdateData(cbPRE.IsChecked.Value, Registro.selectId))
                {
                    MessageBox.Show("Registro actualizado", "Oiga!", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.Close();

                    UpdateGrid(connection);
                }
                else
                {
                    MessageBox.Show("Error al actualizar.", "Oiga!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Marca el check!", "Oiga!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Registro.selectId = 0;
        }
    }
}
