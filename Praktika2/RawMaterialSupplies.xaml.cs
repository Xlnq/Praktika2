using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class RawMaterialSupplies : Page
    {
        RawMaterialSuppliesTableAdapter rawMaterial = new RawMaterialSuppliesTableAdapter();
        UsersTableAdapter user = new UsersTableAdapter();

        public RawMaterialSupplies()
        {
            InitializeComponent();
            Tabl.ItemsSource = rawMaterial.GetData();
            User.ItemsSource = user.GetData();
            User.DisplayMemberPath = "Username";
            User.SelectedValuePath = "UserID";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int user = (int)User.SelectedValue;
            string nameSupply = NameSupply.Text;
            DateTime dateSupply = DateSupply.SelectedDate ?? DateTime.Now;
            string nameRawMaterial = NameRawMaterial.Text;
            decimal sum = Convert.ToDecimal(Sum.Text);
            int quantity = Convert.ToInt32(Quantity.Text);

            bool isDeleted = false;

            rawMaterial.Insert(user, nameSupply, dateSupply, nameRawMaterial, quantity, sum, isDeleted);

            Tabl.ItemsSource = rawMaterial.GetData();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((Tabl.SelectedItem as DataRowView).Row["SupplyID"]);
                rawMaterial.DeleteQuery(id);

                Tabl.ItemsSource = rawMaterial.GetData();
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((Tabl.SelectedItem as DataRowView).Row["SupplyID"]);
                int user = (int)User.SelectedValue;
                string nameSupply = NameSupply.Text;
                DateTime? dateSupply = DateSupply.SelectedDate; 
                string nameRawMaterial = NameRawMaterial.Text;
                decimal sum = Convert.ToDecimal(Sum.Text);
                int quantity = Convert.ToInt32(Quantity.Text);

                bool isDeleted = false;

                rawMaterial.UpdateQuery(user, nameSupply, dateSupply.ToString(), nameRawMaterial, quantity, sum, isDeleted, id);

                Tabl.ItemsSource = rawMaterial.GetData();
            }
        }

        private void RawMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;
                // Получаем значения из выбранной строки и обновляем другие элементы управления

                // Пример: Обновление ComboBox
                int userId = (int)selectedRow["UserID"];
                User.SelectedValue = userId;

                // Пример: Обновление TextBox
                string nameSupply = selectedRow["SupplierName"].ToString();
                NameSupply.Text = nameSupply;

                // Пример: Обновление DatePicker
                DateTime dateSupply = (DateTime)selectedRow["SupplyDate"];
                DateSupply.SelectedDate = dateSupply;

                // Пример: Обновление TextBox для названия сырья
                string nameRawMaterial = selectedRow["MaterialName"].ToString();
                NameRawMaterial.Text = nameRawMaterial;

                // Пример: Обновление TextBox для количества
                int quantity = (int)selectedRow["QuantitySupplied"];
                Quantity.Text = quantity.ToString();

                // Пример: Обновление TextBox для стоимости
                decimal sum = (decimal)selectedRow["Cost"];
                Sum.Text = sum.ToString();

            }
        }

    }
}
