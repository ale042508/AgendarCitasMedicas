using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoFinal
{
    /// <summary>
    /// Interaction logic for VerCita.xaml
    /// </summary>
    public partial class VerCita : Window
    {
        public VerCita()
        {
            InitializeComponent();
            Ver_Cita();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source = localhost; Initial Catalog = ProyectoFinalLP1; Integrated Security=True;");
        private void MenuItem_Home(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
        void Ver_Cita()
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdDoctor = "SELECT Doc_ID FROM Cita";
                SqlCommand sqlCmdIdDoctor = new SqlCommand(queryIdDoctor, sqlCon);
                SqlDataReader readerIdDoctor = sqlCmdIdDoctor.ExecuteReader();
                string IdDoctor = "";
                while (readerIdDoctor.Read())
                {
                    if (readerIdDoctor.GetInt32(0).ToString() != "35")
                    {
                        IdDoctor = readerIdDoctor.GetInt32(0).ToString();
                    }
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdFecha = "SELECT FD_ID FROM Cita";
                SqlCommand sqlCmdIdFecha = new SqlCommand(queryIdFecha, sqlCon);
                SqlDataReader readerIdFecha = sqlCmdIdFecha.ExecuteReader();
                string IdFecha = "";
                while (readerIdFecha.Read())
                {
                    if (readerIdFecha.GetInt32(0).ToString() != "97")
                    {
                        IdFecha = readerIdFecha.GetInt32(0).ToString();
                    }
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryIdCita = $"SELECT Cli_ID FROM Cita Where Doc_ID = {IdDoctor}";
                SqlCommand sqlCmdIdCita = new SqlCommand(queryIdCita, sqlCon);
                SqlDataReader readerIdCita = sqlCmdIdCita.ExecuteReader();
                string idCita = "";
                while (readerIdCita.Read())
                {
                    idCita = readerIdCita.GetInt32(0).ToString();
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryNombreCliente = $"SELECT Cli_ID, Cli_Nombres, Cli_Apellidos FROM Cliente Where Cli_ID = {idCita}";
                SqlCommand sqlCmdNombreCliente = new SqlCommand(queryNombreCliente, sqlCon);
                SqlDataReader readerNombreCliente = sqlCmdNombreCliente.ExecuteReader();
                while (readerNombreCliente.Read())
                {
                    if (readerNombreCliente.GetInt32(0).ToString() != idCita)
                    {
                        lb_VerCita.Items.Add($"Cita para: No disponible\n");
                    }
                    else
                    {
                        string NombreCliente = readerNombreCliente.GetString(1);
                        string apellidoCliente = readerNombreCliente.GetString(2);
                        lb_VerCita.Items.Add($"Cita para: {NombreCliente} {apellidoCliente}\n");
                    }
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryDoctor = $"SELECT Doc_ID, Doc_Nombre FROM Doctores Where Doc_ID = {IdDoctor}";
                SqlCommand sqlCmdDoctor = new SqlCommand(queryDoctor, sqlCon);
                SqlDataReader readerDoctor = sqlCmdDoctor.ExecuteReader();
                while (readerDoctor.Read())
                {
                    if (readerDoctor.GetInt32(0).ToString() != IdDoctor)
                    {
                        lb_VerCita.Items.Add($"Doctor: No disponible\n");
                    }
                    else
                    {
                        string Doctor = readerDoctor.GetString(1);
                        lb_VerCita.Items.Add($"Doctor: {Doctor}\n");
                    }
                }
                sqlCon.Close();
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String queryFechaDisponible = $"SELECT FD_ID, FD_Dia, FD_Tanda FROM FechaDisponible Where FD_ID = {IdFecha}";
                SqlCommand sqlCmdFechaDisponible = new SqlCommand(queryFechaDisponible, sqlCon);
                SqlDataReader readerFechaDisponible = sqlCmdFechaDisponible.ExecuteReader();
                while (readerFechaDisponible.Read())
                {
                    if (readerFechaDisponible.GetInt32(0).ToString() != IdFecha)
                    {
                        lb_VerCita.Items.Add($"Día: No disponible, Tanda: No disponible\n");
                    }
                    else
                    {
                        string Dia = readerFechaDisponible.GetString(1);
                        string Tanda = readerFechaDisponible.GetString(2);
                        lb_VerCita.Items.Add($"Día: {Dia}, Tanda: {Tanda}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Hide();
            main.Show();
        }
    }
}
