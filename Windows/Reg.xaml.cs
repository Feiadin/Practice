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
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Practice.Windows
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        SqlConnection sqlConnection = null;
        public Reg()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            open_eye.Visibility = Visibility.Hidden;
        }

        private void Reg_btn_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || log.Text == "" || pas.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else
            {
                MessageBox.Show("Все поля заполнены!");
                SqlCommand command = new SqlCommand($"select Name from Users where Login like @log", sqlConnection);
                command.Parameters.AddWithValue("log", log.Text);

                DataTable dataTable = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = command;
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count <= 0)
                {
                    SqlCommand command1 = new SqlCommand("Insert into Users (Name, Login, Password) values (@name, @log, @pas)", sqlConnection);
                    command1.Parameters.AddWithValue("name", name.Text);
                    command1.Parameters.AddWithValue("log", log.Text);
                    command1.Parameters.AddWithValue("pas", pas.Text);

                    //MessageBox.Show(command1.ExecuteScalar().ToString());

                    if (command1.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show($"Добро пожаловать, {command.ExecuteScalar()}!");
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не добавлен!");
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                }
            }
        }

        private void Log_lb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Log log = new Log();
            log.Show();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
