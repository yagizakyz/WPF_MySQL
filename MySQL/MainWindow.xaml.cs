using System;
using System.Collections.Generic;
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
using MySql.Data;
using System.Data;
using MySql.Data.MySqlClient;

namespace MySQL
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        //server=localhost;user=root;password=trbzn6161;database=test
        string connectionString = @"server=sql7.freemysqlhosting.net;userid=*****;password=*****;database=sql7344566";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void idTXT_GotFocus(object sender, RoutedEventArgs e)
        {
            idTXT.Clear();
        }

        private void nameTXT_GotFocus(object sender, RoutedEventArgs e)
        {
            nameTXT.Clear();
        }

        private void surnameTXT_GotFocus(object sender, RoutedEventArgs e)
        {
            surnameTXT.Clear();
        }

        public void opens()
        {
            grid.Columns.Clear();
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("select * from denemee", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "LoadDataBinding");
            grid.DataContext = ds;
            connection.Close();
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            opens();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("insert into denemee(name, surname) VALUES (@n,@s)", connection);
            command.Parameters.AddWithValue("@n", nameTXT.Text);
            command.Parameters.AddWithValue("@s", surnameTXT.Text);
            command.ExecuteNonQuery();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "LoadDataBinding");
            grid.DataContext = ds;
            connection.Close();
            opens();
        }
        
        private void update_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("update denemee set name=@n, surname=@s where id=@i", connection);
            command.Parameters.AddWithValue("@n", nameTXT.Text);
            command.Parameters.AddWithValue("@s", surnameTXT.Text);
            command.Parameters.AddWithValue("@i", idTXT.Text);
            command.ExecuteNonQuery();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "LoadDataBinding");
            grid.DataContext = ds;
            connection.Close();
            opens();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "delete denemee where id='" + idTXT.Text + "'";
            command.ExecuteNonQuery();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "LoadDataBinding");
            grid.DataContext = ds;
            connection.Close();
            opens();
        }
    }
}
