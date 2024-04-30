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
    /// Логика взаимодействия для Edit_rest.xaml
    /// </summary>
    public partial class Edit_rest : Window
    {
        SqlConnection sqlConnection = null;

        public Edit_rest()
        {
            InitializeComponent();
        }

        int window = Data.startWindow;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            if (window == 1)
            {
                Name_tb.Text = Data.nameType;
            }
            else
            {
                if (window == 2)
                {
                    Name_tb.Text = Data.countSpeed.ToString();
                }
                else
                {
                    if (window == 3)
                    {
                        Name_tb.Text = Data.nameBrake;
                    }
                }
            }
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Name_tb.Text == "")
            {
                MessageBox.Show("Заполните поле!");
                Name_tb.Focus();
            }
            else
            {
                SqlCommand command = null;
                if (window == 1)
                {
                    command = new SqlCommand("update TypeOfBicycle set Name = @name where Name like @old_name", sqlConnection);
                    command.Parameters.AddWithValue("name", Name_tb.Text);
                    command.Parameters.AddWithValue("old_name", Data.nameType);
                }
                else
                {
                    if (window == 2)
                    {
                        command = new SqlCommand("update Speeds set Count = @count where Count like @old_count", sqlConnection);
                        command.Parameters.AddWithValue("count", Convert.ToInt32(Name_tb.Text));
                        command.Parameters.AddWithValue("old_count", Data.countSpeed);
                    }
                    else
                    {
                        if (window == 3)
                        {
                            command = new SqlCommand("update Brakes set Name = @name where Name like @old_name", sqlConnection);
                            command.Parameters.AddWithValue("name", Name_tb.Text);
                            command.Parameters.AddWithValue("old_name", Data.nameBrake);
                        }
                    }
                }

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные обновлены!");
                    this.Close();
                    Main main = new Main();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Данные не обновлены!");
                }
            }
        }
    }
}
