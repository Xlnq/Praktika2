using System.Data;
using System.Windows;
using System;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class ProductionProcesses : Page
    {
        ProductionProcessesTableAdapter productionProcesses = new ProductionProcessesTableAdapter();

        public ProductionProcesses()
        {
            InitializeComponent();
            Tabl.ItemsSource = productionProcesses.GetData();
        }

        private void Tabl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;

                // Пример: Обновление текстового поля для названия процесса
                string name = selectedRow["ProcessName"].ToString();
                Name.Text = name;

                // Пример: Обновление выбора даты для времени начала
                DateTime startDate = (DateTime)selectedRow["StartTime"];
                DateStart.SelectedDate = startDate;

                // Пример: Обновление выбора даты для времени окончания
                DateTime endDate = (DateTime)selectedRow["EndTime"];
                DateEnd.SelectedDate = endDate;

                // Пример: Обновление текстового поля для описания
                string description = selectedRow["Description"].ToString();
                Discription.Text = description;
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            // Код для добавления записи в таблицу ProductionProcesses
            string name = Name.Text;
            DateTime startDate = DateStart.SelectedDate ?? DateTime.Now;
            DateTime endDate = DateEnd.SelectedDate ?? DateTime.Now;
            string description = Discription.Text;

            productionProcesses.Insert(name, startDate, endDate, description);

            Tabl.ItemsSource = productionProcesses.GetData();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((DataRowView)Tabl.SelectedItem)["ProcessID"];
                productionProcesses.DeleteQuery(id);

                Tabl.ItemsSource = productionProcesses.GetData();
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;
                int id = (int)selectedRow["ProcessID"];
                string name = Name.Text;
                DateTime startDate = DateStart.SelectedDate ?? DateTime.Now;
                DateTime endDate = DateEnd.SelectedDate ?? DateTime.Now;
                string description = Discription.Text;
                bool isDeleted = false;

                productionProcesses.UpdateQuery(name, startDate.ToString(), endDate.ToString(), description, id);

                Tabl.ItemsSource = productionProcesses.GetData();
            }
        }
    }
}
