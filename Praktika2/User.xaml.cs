using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class User : Page
    {
        UsersTableAdapter users = new UsersTableAdapter();

        public User()
        {
            InitializeComponent();
            Tabl.ItemsSource = users.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Добавление нового пользователя
            string login = Login.Text;
            string password = Password.Text;
            string role = Role.Text;
            string email = Email.Text;

            bool isDeleted = false;

            users.Insert(login, password, role, email, isDeleted);

            // Обновление отображаемых данных в DataGrid
            Tabl.ItemsSource = users.GetData();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Удаление выбранного пользователя
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((Tabl.SelectedItem as DataRowView).Row["UserID"]);
                users.DeleteQuery(id);
                Tabl.ItemsSource = users.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Изменение данных пользователя
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((Tabl.SelectedItem as DataRowView).Row["ID"]);
                string login = Login.Text;
                string password = Password.Text;
                string role = Role.Text;
                string email = Email.Text;

                bool isDeleted = false;

                users.UpdateQuery(login, password, role, email, isDeleted, id);
                Tabl.ItemsSource = users.GetData();
            }
        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка изменения выбранного пользователя
            if (Tabl.SelectedItem != null)
            {
                var selectedUser = Tabl.SelectedItem as DataRowView;
                Login.Text = (string)selectedUser.Row["Username"];
                Password.Text = (string)selectedUser.Row["Password"];
                Role.Text = (string)selectedUser.Row["UserRole"];
                Email.Text = (string)selectedUser.Row["Email"];
            }
        }
    }
}
