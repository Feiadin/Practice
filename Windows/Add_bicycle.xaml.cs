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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        SqlConnection sqlConnection = null;

        public Add()
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
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
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
                                SqlCommand command = new SqlCommand("select * from Bicycles where Name = @name and Model = @model", sqlConnection);
                                command.Parameters.AddWithValue("name", Name_tb.Text);
                                command.Parameters.AddWithValue("model", Model_tb.Text);

                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                if (table.Rows.Count <= 0)
                                {
                                    command = new SqlCommand("insert into Bicycles (Name, Model, Type, CountSpeed, TypeBrake) values (@name, @model, @type, @speed, @brake)", sqlConnection);

                                    command.Parameters.AddWithValue("name", Name_tb.Text);
                                    command.Parameters.AddWithValue("model", Model_tb.Text);
                                    command.Parameters.AddWithValue("type", Type_cb.SelectedIndex + 1);
                                    command.Parameters.AddWithValue("speed", Speed_cb.SelectedIndex + 1);
                                    command.Parameters.AddWithValue("brake", Brake_cb.SelectedIndex + 1);

                                    if (command.ExecuteNonQuery() == 1)
                                    {
                                        MessageBox.Show("Велосипед добавлен!");
                                        Data.startWindow = 0;
                                        this.Close();
                                        Main main = new Main();
                                        main.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Велосипед не добавлен!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Такой велосипед уже добавлен!");
                                    Name_tb.Focus();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
