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
using System.Data.SqlClient;

namespace WpfApp1
{
    public partial class NewTaskWindow : Window
    {
        public NewTaskWindow()
        {
            InitializeComponent();
            date1.Text = Convert.ToString(DateTime.Now);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int id_d = getID();
            string connectionString = string.Format("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=NotesDB;Integrated Security=True");
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                DateTime time = datePicker.SelectedDate.Value;
                string commandText1 = string.Format("INSERT INTO  DETAILS.REMINDERS (CUSTOMERID, ReminderID, Theme, TextReminder, DateCreated, DateToDo) VALUES ( @CUSTOMERID, @ReminderID,@Theme,@TextReminder,@DateCreated,@DateToDo)") ;
                SqlCommand command1 = new SqlCommand(commandText1, connection);
                command1.Parameters.AddWithValue("@CUSTOMERID", 1);
                command1.Parameters.AddWithValue("@ReminderID", id_d);
                command1.Parameters.AddWithValue("@Theme", themeBox.Text);
                command1.Parameters.AddWithValue("@TextReminder", newTaskBox.Text);
                command1.Parameters.AddWithValue("@DateCreated", date1.Text); 
                command1.Parameters.AddWithValue("@DateToDo", time.ToString("yyyy-MM-dd"));

                command1.ExecuteNonQuery();
                    connection.Close();
                }
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder results = new StringBuilder();
                DateTime time = datePicker.SelectedDate.Value;
                string commandText2 = string.Format("SELECT ReminderID, Theme, TextReminder, DateCreated, DateToDo FROM DETAILS.REMINDERS WHERE Date = ('{0}') ORDER BY DateCreated DESC", datePicker);
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
                newTaskBox.Text = results.ToString();
                reader.Close();
                connection.Close();
            }
            this.Close();
        }

        private int getID()
        {
            string connectionString = string.Format("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=NotesDB;Integrated Security=True");
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("SELECT MAX(ReminderID) FROM DETAILS.REMINDERS", connection);
                int id = Convert.ToInt32(command1.ExecuteScalar()) + 1;
                connection.Close();
                return id;
            }
            
        }
        private void timeBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void newTaskBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
