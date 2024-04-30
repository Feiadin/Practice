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
using System.Windows.Markup;

namespace Practice.Windows
{
    /// <summary>
    /// Логика взаимодействия для Log.xaml
    /// </summary>
    public partial class Log : Window
    {
        SqlConnection sqlConnection = null;
        public Log()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            open_eye.Visibility = Visibility.Hidden;
        }

        private void Show_btn_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand("select Name from Users where Login like @log and Password like @pas", sqlConnection);

            command.Parameters.AddWithValue("log", Log_tb.Text);
            command.Parameters.AddWithValue("pas", Pas_tb.Text);

            DataTable dataTable = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (Log_tb.Text == "" || Pas_tb.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else
            {
                if (dataTable.Rows.Count != 0)
                {
                    MessageBox.Show($"Добро пожаловать, {command.ExecuteScalar()}!");
                    //Data.Login = Log_tb.Text;
                    //this.Hide();
                    //Main main = new Main();
                    //main.Show();
                    this.Hide();
                    Main main = new Main();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль!");
                }
            }
        }

        private void Reg_lb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Reg reg = new Reg();
            reg.Show();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
