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
    /// Логика взаимодействия для Edit_bicycle.xaml
    /// </summary>
    public partial class Edit_bicycle : Window
    {
        SqlConnection sqlConnection = null;

        public Edit_bicycle()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            //Заполнение типов велосипедов

            SqlCommand command = new SqlCommand("Select Name from TypeOfBicycle", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            Type_cb.DisplayMemberPath = "Name";

            Type_cb.ItemsSource = table.DefaultView;

            //Заполнение скоростей

            command = new SqlCommand("Select Count from Speeds", sqlConnection);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);

            Speed_cb.DisplayMemberPath = "Count";

            Speed_cb.ItemsSource = table.DefaultView;

            //Заполнение типов тормозов

            command = new SqlCommand("Select Name from Brakes", sqlConnection);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);

            Brake_cb.DisplayMemberPath = "Name";

            Brake_cb.ItemsSource = table.DefaultView;

            //Заполнение полей

            Name_tb.Text = Data.nameBicycle;
            Model_tb.Text = Data.modelBicycle;
            Type_cb.SelectedIndex = Data.typeBicycle -1;
            Speed_cb.SelectedIndex = Data.speedBicycle -1;
            Brake_cb.SelectedIndex = Data.brakeBicycle - 1;
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Name_tb.Text == "")
            {
                MessageBox.Show("Введите название велосипеда!");
                Name_tb.Focus();
            }
            else
            {
                if (Model_tb.Text == "")
                {
                    MessageBox.Show("Введите модель велосипеда!");
                    Model_tb.Focus();
                }
                else
                {
                    if (Type_cb.SelectedItem == null)
                    {
                        MessageBox.Show("Выберите тип велосипеда!");
                        Type_cb.Focus();
                    }
                    else
                    {
                        if (Speed_cb.SelectedItem == null)
                        {
                            MessageBox.Show("Выберите количество скоростей велосипеда!");
                            Speed_cb.Focus();
                        }
                        else
                        {
                            if (Brake_cb.SelectedItem == null)
                            {
                                MessageBox.Show("Выберите тип тормозов велосипеда!");
                                Brake_cb.Focus();
                            }
                            else
                            {
                                SqlCommand command = new SqlCommand("update Bicycles set Name = @name, Model = @model, Type = @type, CountSpeed = @speed, TypeBrake = @brake where Name like @old_name and Model like @old_model", sqlConnection);

                                command.Parameters.AddWithValue("name", Name_tb.Text);
                                command.Parameters.AddWithValue("model", Model_tb.Text);
                                command.Parameters.AddWithValue("type", Type_cb.SelectedIndex + 1);
                                command.Parameters.AddWithValue("speed", Speed_cb.SelectedIndex + 1);
                                command.Parameters.AddWithValue("brake", Brake_cb.SelectedIndex + 1);
                                command.Parameters.AddWithValue("old_name", Data.nameBicycle);
                                command.Parameters.AddWithValue("old_model", Data.modelBicycle);

                                if (command.ExecuteNonQuery() == 1)
                                {
                                    MessageBox.Show("Данныые о велосипеде обновлены!");
                                    Data.startWindow = 0;
                                    this.Close();
                                    Main main = new Main();
                                    main.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Данныые о велосипеде не обновлены!");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
