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
    /// Логика взаимодействия для Add_rest.xaml
    /// </summary>
    public partial class Add_rest : Window
    {
        SqlConnection sqlConnection = null;

        public Add_rest()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            int window = Data.startWindow;
            if (Name_tb.Text == "")
            {
                if (window == 1)
                {
                    MessageBox.Show("Введите название типа велосипеда!");
                }
                else
                {
                    if (window == 2)
                    {
                        MessageBox.Show("Введите количество скоростей!");
                    }
                    else
                    {
                        if (window == 3)
                        {
                            MessageBox.Show("Введите название типа тормозов велосипеда!");
                        }
                    }
                }
                Name_tb.Focus();
            }
            else
            {
                SqlCommand command = null;
                if (window == 1)
                {
                    command = new SqlCommand("select * from TypeOfBicycle where Name = @name", sqlConnection);
                    command.Parameters.AddWithValue("name", Name_tb.Text);
                }
                else
                {
                    if (window == 2)
                    {
                        command = new SqlCommand("select * from Speeds where Count = @count", sqlConnection);
                        command.Parameters.AddWithValue("count", Convert.ToInt32(Name_tb.Text));
                    }
                    else
                    {
                        if (window == 3)
                        {
                            command = new SqlCommand("select * from Brakes where Name = @name", sqlConnection);
                            command.Parameters.AddWithValue("name", Name_tb.Text);
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count <= 0)
                {
                    if (window == 1)
                    {
                        command = new SqlCommand("insert into TypeOfBicycle (Name) values (@name)", sqlConnection);
                        command.Parameters.AddWithValue("name", Name_tb.Text);

                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Тип велосипеда добавлен!");
                            Data.startWindow = 1;
                            this.Close();
                            Main main = new Main();
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Тип велосипеда не добавлен!");
                        }
                    }
                    else
                    {
                        if (window == 2)
                        {
                            command = new SqlCommand("insert into Speeds (Count) values (@count)", sqlConnection);
                            command.Parameters.AddWithValue("count", Name_tb.Text);

                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Скорость добавлена!");
                                Data.startWindow = 2;
                                this.Close();
                                Main main = new Main();
                                main.Show();
                            }
                            else
                            {
                                MessageBox.Show("Скорость не добавлена!");
                            }
                        }
                        else
                        {
                            if (window == 3)
                            {
                                command = new SqlCommand("insert into Brakes (Name) values (@name)", sqlConnection);
                                command.Parameters.AddWithValue("name", Name_tb.Text);

                                if (command.ExecuteNonQuery() == 1)
                                {
                                    MessageBox.Show("Тип тормозов велосипеда добавлен!");
                                    Data.startWindow = 3;
                                    this.Close();
                                    Main main = new Main();
                                    main.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Тип тормозов велосипеда не добавлен!");
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (window == 1)
                    {
                        MessageBox.Show("Такой типа велосипеда уже есть!");
                    }
                    else
                    {
                        if (window == 2)
                        {
                            MessageBox.Show("Такая скорость уже есть!");
                        }
                        else
                        {
                            if (window == 3)
                            {
                                MessageBox.Show("Такой типа тормозов велосипеда уже есть!");
                            }
                        }
                    }
                    Name_tb.Focus();
                }
            }
        }
    }
}
