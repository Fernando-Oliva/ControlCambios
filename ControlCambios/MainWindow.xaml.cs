using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ControlCambios.Views;
using ControlCambios.Core;
using ControlCambios.Clases;

namespace ControlCambios
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        controlCambiosDataSet ccds = new controlCambiosDataSet();

        public MainWindow()
        {
            InitializeComponent();

            DBLayer connectLayer = new DBLayer();

            dgChanges.DataContext = connectLayer.getAllRows();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow chgWin = new ChangeWindow();

            chgWin.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            DBLayer connectLayer = new DBLayer();

            dgChanges.DataContext = null;
            dgChanges.DataContext = connectLayer.updateRows();
        }

        private void dgChanges_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = (int)dgChanges.SelectedIndex;

            CheckBox isInPRE = dgChanges.Columns[3].GetCellContent(dgChanges.Items[index]) as CheckBox;

            if (!isInPRE.IsChecked.Value)
            {
                TextBlock aux = dgChanges.Columns[0].GetCellContent(dgChanges.Items[index]) as TextBlock;

                Registro.selectId = Convert.ToInt32(aux.Text);

                ChangeWindow changeWin = new ChangeWindow();

                changeWin.Show();
            }
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow config = new ConfigWindow();

            config.Show();
        }
    }
}
