using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class QualityMonitoring : Page
    {
        QualityMonitoringTableAdapter quality = new QualityMonitoringTableAdapter();
        RawMaterialSuppliesTableAdapter rawMaterial = new RawMaterialSuppliesTableAdapter();
        ProductsTableAdapter products = new ProductsTableAdapter();
        ProductQualityStatusTableAdapter qualityStatus = new ProductQualityStatusTableAdapter();

        public QualityMonitoring()
        {
            InitializeComponent();
            Tabl.ItemsSource = quality.GetData();
            Product.ItemsSource = products.GetData();
            RawMaterial.ItemsSource = rawMaterial.GetData();
            QualityStatus.ItemsSource = qualityStatus.GetData();
            Product.DisplayMemberPath = "ProductName";
            Product.SelectedValuePath = "ProductID";
            RawMaterial.DisplayMemberPath = "MaterialName";
            RawMaterial.SelectedValuePath = "SupplyID";
            QualityStatus.DisplayMemberPath = "QualityStatusName";
            QualityStatus.SelectedValuePath = "QualityStatusID";
        }

        private void Tabl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;

                // Обновление ComboBox для продукта
                int productId = (int)selectedRow["ProductID"];
                Product.SelectedValue = productId;

                // Обновление ComboBox для сырья
                int rawMaterialId = (int)selectedRow["SupplyID"];
                RawMaterial.SelectedValue = rawMaterialId;

                // Обновление DatePicker для даты проверки
                DateTime dateCheck = (DateTime)selectedRow["InspectionDate"];
                DateCheck.SelectedDate = dateCheck;

                // Обновление ComboBox для статуса качества
                int qualityStatusId = (int)selectedRow["QualityStatusID"];
                QualityStatus.SelectedValue = qualityStatusId;
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            // Код для добавления записи в таблицу QualityMonitoring
            int productId = (int)Product.SelectedValue;
            int rawMaterialId = (int)RawMaterial.SelectedValue;
            DateTime dateCheck = DateCheck.SelectedDate ?? DateTime.Now;
            int qualityStatusId = (int)QualityStatus.SelectedValue;
            bool isDeleted = false;

            quality.Insert(productId, dateCheck, qualityStatusId, rawMaterialId,isDeleted);

            Tabl.ItemsSource = quality.GetData();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((DataRowView)Tabl.SelectedItem)["MonitoringID"];
                quality.DeleteQuery(id);

                Tabl.ItemsSource = quality.GetData();
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;
                int id = (int)selectedRow["MonitoringID"];
                int productId = (int)Product.SelectedValue;
                int rawMaterialId = (int)RawMaterial.SelectedValue;
                DateTime dateCheck = DateCheck.SelectedDate ?? DateTime.Now;
                int qualityStatusId = (int)QualityStatus.SelectedValue;
                bool isDeleted = false;

                quality.UpdateQuery(productId, dateCheck.ToString(), qualityStatusId, rawMaterialId, isDeleted, id);

                Tabl.ItemsSource = quality.GetData();
            }
        }
    }
}
