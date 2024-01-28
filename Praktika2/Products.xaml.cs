using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class Products : Page
    {
        ProductsTableAdapter products = new ProductsTableAdapter();
        ProductCategoryTableAdapter categories = new ProductCategoryTableAdapter();
        ProductionProcessesTableAdapter productionProcesses = new ProductionProcessesTableAdapter();

        public Products()
        {
            InitializeComponent();
            Tabl.ItemsSource = products.GetData();
            Category.ItemsSource = categories.GetData();
            Process.ItemsSource = productionProcesses.GetData();
            Category.DisplayMemberPath = "CategoryName";
            Category.SelectedValuePath = "CategoryID";
            Process.DisplayMemberPath = "ProcessName";
            Process.SelectedValuePath = "ProcessID";
        }

        private void Tabl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;

                // Обновление текстового поля для названия продукта
                string productName = (string)selectedRow["ProductName"];
                Name.Text = productName;

                // Обновление ComboBox для категории
                int categoryId = (int)selectedRow["CategoryID"];
                Category.SelectedValue = categoryId;

                // Обновление текстового поля для цены
                decimal price = (decimal)selectedRow["Price"];
                Price.Text = price.ToString();

                // Обновление текстового поля для количества на складе
                int quantity = (int)selectedRow["QuantityInStock"];
                Quantity.Text = quantity.ToString();

                // Обновление ComboBox для производственного процесса
                int processId = (int)selectedRow["ProductionProcessID"];
                Process.SelectedValue = processId;
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            // Код для добавления записи в таблицу Products
            string productName = Name.Text;
            int categoryId = (int)Category.SelectedValue;
            decimal price = decimal.Parse(Price.Text);
            int quantity = int.Parse(Quantity.Text);
            int processId = (int)Process.SelectedValue;
            bool isDeleted = false;

            products.Insert(productName, categoryId, price, quantity, processId, isDeleted);

            Tabl.ItemsSource = products.GetData();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((DataRowView)Tabl.SelectedItem)["ProductID"];
                products.DeleteQuery(id);

                Tabl.ItemsSource = products.GetData();
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;
                int id = (int)selectedRow["ProductID"];
                string productName = Name.Text;
                int categoryId = (int)Category.SelectedValue;
                decimal price = decimal.Parse(Price.Text);
                int quantity = int.Parse(Quantity.Text);
                int processId = (int)Process.SelectedValue;
                bool isDeleted = false;

                products.UpdateQuery(productName, categoryId, price, quantity, processId, isDeleted, id);

                Tabl.ItemsSource = products.GetData();
            }
        }
    }
}
