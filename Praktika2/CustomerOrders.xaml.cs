using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class CustomerOrders : Page
    {
        CustomerOrdersTableAdapter customerOrders = new CustomerOrdersTableAdapter();
        UsersTableAdapter users = new UsersTableAdapter();
        OrderProductsTableAdapter orderProducts = new OrderProductsTableAdapter();
        OrderStatusTableAdapter orders = new OrderStatusTableAdapter();

        public CustomerOrders()
        {
            InitializeComponent();
            Tabl.ItemsSource = customerOrders.GetData();
            User.ItemsSource = users.GetData();
            User.DisplayMemberPath = "Username";
            User.SelectedValuePath = "UserID";
            Order.ItemsSource = orderProducts.GetData();
            Order.DisplayMemberPath = "OrderID";
            Order.SelectedValuePath = "OrderID";
            Status.ItemsSource = orders.GetData();
            Status.DisplayMemberPath = "StatusName";
            Status.SelectedValuePath = "OrderStatusID";
        }

        private void Tabl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;

                int userId = (int)selectedRow["UserID"];
                User.SelectedValue = userId;

                int orderId = selectedRow["OrderID"] as int? ?? 0;
                Order.SelectedValue = orderId;

                DateTime datePost = (DateTime)selectedRow["OrderDate"];
                DatePsot.SelectedDate = datePost;

                DateTime dateDelivery = (DateTime)selectedRow["DeliveryDate"];
                DateDelivery.SelectedDate = dateDelivery;

                decimal amount = (decimal)selectedRow["TotalAmount"];
                Amount.Text = amount.ToString();

                string status = selectedRow["OrderStatusID"].ToString();
                Status.SelectedValue = status;
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            int userId = (int)User.SelectedValue;
            int orderId = (int)Order.SelectedValue;
            DateTime datePost = DatePsot.SelectedDate ?? DateTime.Now;
            DateTime dateDelivery = DateDelivery.SelectedDate ?? DateTime.Now;
            decimal amount = decimal.Parse(Amount.Text);
            int status = Convert.ToInt32(Status.SelectedValue);
            bool isDeleted = false;

            customerOrders.Insert(userId, orderId, datePost, dateDelivery, amount, status, isDeleted);

            Tabl.ItemsSource = customerOrders.GetData();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((DataRowView)Tabl.SelectedItem)["OrderCustomerID"];
                customerOrders.DeleteQuery(id);

                Tabl.ItemsSource = customerOrders.GetData();
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;
                int id = (int)selectedRow["OrderCustomerID"];
                int userId = (int)User.SelectedValue;
                int orderId = (int)Order.SelectedValue;
                DateTime datePost = DatePsot.SelectedDate ?? DateTime.Now;
                DateTime dateDelivery = DateDelivery.SelectedDate ?? DateTime.Now;
                decimal amount = decimal.Parse(Amount.Text);
                int status = Convert.ToInt32(Status.SelectedValue);
                bool isDeleted = false;

                customerOrders.UpdateQuery(userId, orderId, datePost.ToString(), dateDelivery.ToString(), amount, status, isDeleted, id);

                Tabl.ItemsSource = customerOrders.GetData();
            }
        }
        private void Button_Click_Export(object sender, RoutedEventArgs e)
        {

            if (Tabl.SelectedItems.Count == 0)
            {
                MessageBox.Show("Нет выбранных элементов для экспорта.");
                return;
            }

            StringBuilder data = new StringBuilder();

            foreach (var selectedItem in Tabl.SelectedItems)
            {
                DataRowView row = (DataRowView)selectedItem;

                int status = (int)row["OrderStatusID"];
                if (status == 1)
                {
                    data.AppendLine($"                  Чек:");
                    data.AppendLine($"      Клиент: {row["UserID"]}");
                    data.AppendLine($"      Заказ: {row["OrderID"]}");
                    data.AppendLine($"      Дата размещения: {((DateTime)row["OrderDate"]).ToString("dd.MM.yyyy")}");
                    data.AppendLine($"      Дата доставки: {((DateTime)row["DeliveryDate"]).ToString("dd.MM.yyyy")}");
                    data.AppendLine($"      Общая сумма: {row["TotalAmount"]}");
                    data.AppendLine($"      Статус: {row["OrderStatusID"]}");
                    data.AppendLine();
                }
                else
                {
                    MessageBox.Show("Продукт не готов.");
                    return;
                }
            }

            try
            {
                string filePath = "C:\\Users\\mrpor\\Desktop\\Чеки\\Чек.txt";
                File.WriteAllText(filePath, data.ToString());
                MessageBox.Show("Выбранные данные успешно экспортированы в файл " + filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при экспорте выбранных данных: " + ex.Message);
            }
        }

    }
}
