using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class OrderProducts : Page
    {
        OrderProductsTableAdapter orderProducts = new OrderProductsTableAdapter();
        ProductsTableAdapter products = new ProductsTableAdapter();

        public OrderProducts()
        {
            InitializeComponent();
            Tabl.ItemsSource = orderProducts.GetData();
            Product.ItemsSource = products.GetData();
            Product.DisplayMemberPath = "ProductName";
            Product.SelectedValuePath = "ProductID";
        }

        private void Tabl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;

                // Пример: Обновление ComboBox для продукта
                int productId = (int)selectedRow["ProductID"];
                Product.SelectedValue = productId;

                // Пример: Обновление текстового поля для количества
                int quantity = (int)selectedRow["QuantityOrdered"];
                Quantity.Text = quantity.ToString();
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            // Код для добавления записи в таблицу OrderProducts
            int productId = (int)Product.SelectedValue;
            int quantity = int.Parse(Quantity.Text); // Парсим строку в целое число
            bool isDeleted = false;

            orderProducts.Insert(productId, quantity, isDeleted);

            Tabl.ItemsSource = orderProducts.GetData();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((DataRowView)Tabl.SelectedItem)["OrderID"];
                orderProducts.DeleteQuery(id);

                Tabl.ItemsSource = orderProducts.GetData();
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;
                int id = (int)selectedRow["OrderID"];
                int productId = (int)Product.SelectedValue;
                int quantity = int.Parse(Quantity.Text); // Парсим строку в целое число
                bool isDeleted = false;

                orderProducts.UpdateQuery(productId, quantity, isDeleted, id);

                Tabl.ItemsSource = orderProducts.GetData();
            }
        }
    }
}
