using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class MainWindow : Window
    {
        private UsersTableAdapter usersAdapter = new UsersTableAdapter();
        private Dictionary<string, List<Page>> rolePages = new Dictionary<string, List<Page>>();
        private Page currentPage;
        private List<Page> userPages;

        public MainWindow()
        {
            InitializeComponent();
            InitializeRolePages();
        }

        private void InitializeRolePages()
        {
            Next.Visibility = Visibility.Hidden;
            Back.Visibility = Visibility.Hidden;

            rolePages.Add("Administrator", new List<Page> { new User(), new CostTracking(), new OrderProducts(), new Products(), new QualityMonitoring(), new RawMaterialSupplies(), new CostTracking(), new ExponseType(), new CustomerOrders(), new OrderStatus(), new ProductionProcesses(), new ProductCategory(), new ProductQualityStatus() });
            rolePages.Add("User", new List<Page> { new OrderProducts() });
            rolePages.Add("Manager", new List<Page> { new Products(), new QualityMonitoring(), new ProductQualityStatus(), new ProductCategory() });
            rolePages.Add("Purchaser", new List<Page> { new RawMaterialSupplies() });
            rolePages.Add("Accountant", new List<Page> { new CostTracking(), new ExponseType() });
            rolePages.Add("Cashier", new List<Page> { new CustomerOrders(), new OrderStatus() });
            rolePages.Add("Worker", new List<Page> { new ProductionProcesses() });
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string username = Login.Text;
            string password = Password.Password;

            var user = usersAdapter.GetData().FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Next.Visibility = Visibility.Visible;
                Back.Visibility = Visibility.Visible;
                string role = user.UserRole;

                SetUserRolePages(role);

                LoginGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.");
            }
        }

        private void SetUserRolePages(string role)
        {
            if (rolePages.ContainsKey(role))
            {
                userPages = rolePages[role];
                currentPage = userPages[0];
                Auth.Content = currentPage;
            }
            else
            {
                MessageBox.Show("Неверная роль пользователя.");
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = userPages.IndexOf(currentPage);

            if (currentIndex < userPages.Count - 1)
            {
                currentPage = userPages[currentIndex + 1];
                Auth.Content = currentPage;
            }
        }

        private void BackwardButton_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = userPages.IndexOf(currentPage);
            if (currentIndex > 0)
            {
                currentPage = userPages[currentIndex - 1];
                Auth.Content = currentPage;
            }
        }
    }
}
