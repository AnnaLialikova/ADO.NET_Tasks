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
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.f1 = this;
            InitializeComponent();
            var time = DateTime.Today;
            string a = time.ToString("yyyy-mm-dd");
            
            TextBox1.Text = a.ToString();
            string connectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=NotesDB;Integrated Security=True");
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            StringBuilder results = new StringBuilder();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                string commandText2 = string.Format("SELECT CUSTOMERID, TextReminder, DateCreated FROM DETAILS.REMINDERS WHERE DateCreated = GETDATE()") ;
                    SqlCommand command2 = new SqlCommand(commandText2, connection);
                    SqlDataReader reader = command2.ExecuteReader();
                while (reader.Read())
                {
                   for (int i = 0; i < reader.FieldCount; i++)
                    {
                        results.Append(reader[i].ToString() + "\t");
                    }
                    results.Append(Environment.NewLine);

                }
                outTextBox.Text = results.ToString();
                reader.Close();
                    connection.Close();
                }
            

        }


        private void Create_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow NW = new NewTaskWindow();
            NW.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {

        }

        private void getPremium_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FAQ_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Archive_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Notes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            App.f1.outTextBox.Text = null;
            string connectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=NotesDB;Integrated Security=True");
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            StringBuilder results = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DateTime time = datePicker.SelectedDate.Value;
                string commandText2 = string.Format("SELECT CUSTOMERID, TextReminder, DateCreated FROM DETAILS.REMINDERS WHERE DateCreated = '" + time.ToString("yyyy-MM-dd") + "'");
                SqlCommand command2 = new SqlCommand(commandText2, connection);
                SqlDataReader reader = command2.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        results.Append(reader[i].ToString() + "\t");
                    }
                    results.Append(Environment.NewLine);

                }
                outTextBox.Text = results.ToString();
                reader.Close();
                connection.Close();
            }
        }
    }
}
