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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        SqlConnection sqlConnection = null;

        public Main()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            //Заполнение таблицы велосипедов

            SqlCommand command = new SqlCommand("select Bicycles.Name as 'Название', Bicycles.Model as 'Модель', TypeOfBicycle.Name as 'Тип', Speeds.Count as 'Скорости', Brakes.Name as 'Тормоза' from Bicycles, TypeOfBicycle, Speeds, Brakes where Bicycles.Type = TypeOfBicycle.Id and Bicycles.CountSpeed = Speeds.Id and Bicycles.TypeBrake = Brakes.Id", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Bicycles_dg.ItemsSource = dataTable.DefaultView;

            //Заполнение таблицы типов велосипедов

            command = new SqlCommand("select Name as 'Тип' from TypeOfBicycle", sqlConnection);
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            Type_dg.ItemsSource = dataTable.DefaultView;

            //Заполнение таблицы скоростей

            command = new SqlCommand("select Count as 'Количество' from Speeds", sqlConnection);
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            Speed_dg.ItemsSource = dataTable.DefaultView;

            //Заполнение таблицы типов тормозов

            command = new SqlCommand("select Name as 'Тип тормозов' from Brakes", sqlConnection);
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            Brake_dg.ItemsSource = dataTable.DefaultView;

            tab.SelectedIndex = Data.startWindow;

            EditDelete.Visibility = Visibility.Hidden;
            Edit_type.Visibility = Visibility.Hidden;
            Edit_speed.Visibility = Visibility.Hidden;
            Edit_brake.Visibility = Visibility.Hidden;
        }

        private void Bicycles_dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlCommand command = new SqlCommand("select Name as 'Название', Model as 'Модель', Type as 'Тип', CountSpeed as 'Скорости', TypeBrake as 'Тормоза' from Bicycles", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Data.nameBicycle = dataTable.DefaultView[Bicycles_dg.SelectedIndex]["Название"].ToString();
            Data.modelBicycle = dataTable.DefaultView[Bicycles_dg.SelectedIndex]["Модель"].ToString();
            Data.typeBicycle = Convert.ToInt32(dataTable.DefaultView[Bicycles_dg.SelectedIndex]["Тип"]);
            Data.speedBicycle = Convert.ToInt32(dataTable.DefaultView[Bicycles_dg.SelectedIndex]["Скорости"]);
            Data.brakeBicycle = Convert.ToInt32(dataTable.DefaultView[Bicycles_dg.SelectedIndex]["Тормоза"]);

            EditDelete.Visibility = Visibility.Visible;
        }

        private void Add_bicycles_Click(object sender, RoutedEventArgs e)
        {
            Add add = new Add();
            add.ShowDialog();
        }

        private void Edit_bicycles_Click(object sender, RoutedEventArgs e)
        {
            Edit_bicycle edit_Bicycle = new Edit_bicycle();
            edit_Bicycle.ShowDialog();
        }

        private void Delete_bicycles_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand("delete from Bicycles where Name like @name and Model like @model", sqlConnection);
            command.Parameters.AddWithValue("name", Data.nameBicycle);
            command.Parameters.AddWithValue("model", Data.modelBicycle);

            command.ExecuteNonQuery();

            MessageBox.Show("Велосипед удалён!");

            this.Close();
            Main main = new Main();
            main.Show();
        }

        private void Type_dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlCommand command = new SqlCommand("select Name as 'Тип' from TypeOfBicycle", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Data.nameType = dataTable.DefaultView[Type_dg.SelectedIndex]["Тип"].ToString();
            Data.startWindow = 1;

            Edit_type.Visibility = Visibility.Visible;
        }

        private void Add_type_Click(object sender, RoutedEventArgs e)
        {
            Data.startWindow = 1;
            //MessageBox.Show(Data.startWindow.ToString());
            Add_rest add_Rest = new Add_rest();
            add_Rest.ShowDialog();
        }

        private void Edit_type_Click(object sender, RoutedEventArgs e)
        {
            Edit_rest edit_Rest = new Edit_rest();
            edit_Rest.ShowDialog();
        }

        private void Speed_dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlCommand command = new SqlCommand("select Count as 'Количество' from Speeds", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Data.countSpeed = Convert.ToInt32(dataTable.DefaultView[Speed_dg.SelectedIndex]["Количество"]);
            Data.startWindow = 2;

            Edit_speed.Visibility = Visibility.Visible;
        }

        private void Add_speed_Click(object sender, RoutedEventArgs e)
        {
            Data.startWindow = 2;
            //MessageBox.Show(Data.startWindow.ToString());
            Add_rest add_Rest = new Add_rest();
            add_Rest.ShowDialog();
        }

        private void Edit_speed_Click(object sender, RoutedEventArgs e)
        {
            Edit_rest edit_Rest = new Edit_rest();
            edit_Rest.ShowDialog();
        }

        private void Brake_dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlCommand command = new SqlCommand("select Name as 'Тип тормозов' from Brakes", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Data.nameBrake = dataTable.DefaultView[Brake_dg.SelectedIndex]["Тип тормозов"].ToString();
            Data.startWindow = 3;

            Edit_brake.Visibility = Visibility.Visible;
        }

        private void Add_brake_Click(object sender, RoutedEventArgs e)
        {
            Data.startWindow = 3;
            //MessageBox.Show(Data.startWindow.ToString());
            Add_rest add_Rest = new Add_rest();
            add_Rest.ShowDialog();
        }

        private void Edit_brake_Click(object sender, RoutedEventArgs e)
        {
            Edit_rest edit_Rest = new Edit_rest();
            edit_Rest.ShowDialog();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
