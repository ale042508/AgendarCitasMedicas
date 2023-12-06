using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source = localhost; Initial Catalog = ProyectoFinalLP1; Integrated Security=True;");
        private void MenuItem_Home(object sender, RoutedEventArgs e)
        {
            AgendarCitas agendarCitas = new AgendarCitas();
            agendarCitas.Close();
            this.Show();
        }
        private void Boton_Exit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string queryIdCliente = $"Delete From Cita Where Doc_ID = @Doc_ID";
                SqlCommand comando = new SqlCommand(queryIdCliente, sqlCon);
                comando.Parameters.AddWithValue("@Doc_ID", "35");
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            Application.Current.Shutdown();
        }
        private void Boton_Agendar_Cita(object sender, RoutedEventArgs e)
        {
            AgendarCitas agendarCitas = new AgendarCitas();
            this.Hide();
            agendarCitas.Show();
        }
        private void Boton_Ver_Cita(object sender, RoutedEventArgs e)
        {
            VerCita verCita = new VerCita();
            this.Hide();
            verCita.Show();
        }
    }
}