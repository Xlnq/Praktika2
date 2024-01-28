using System.Data;
using System.Windows;
using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class CostTracking : Page
    {
        CostTrackingTableAdapter costTracking = new CostTrackingTableAdapter();
        ProductionProcessesTableAdapter productionProcesses = new ProductionProcessesTableAdapter();
        ExpenseTypeTableAdapter expenseType = new ExpenseTypeTableAdapter();

        public CostTracking()
        {
            InitializeComponent();
            Tabl.ItemsSource = costTracking.GetData();
            Process.ItemsSource = productionProcesses.GetData();
            Process.DisplayMemberPath = "ProcessName";
            Process.SelectedValuePath = "ProcessID";
            ExponceType.ItemsSource = expenseType.GetData();
            ExponceType.DisplayMemberPath = "ExpenseTypeName";
            ExponceType.SelectedValuePath = "ExpenseTypeID";
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            // Код для добавления записи в таблицу CostTracking
            int process = (int)Process.SelectedValue;
            int expenseType = (int)ExponceType.SelectedValue;
            decimal sum = decimal.Parse(Sum.Text);
            bool isDeleted = false;

            costTracking.Insert(process, expenseType, sum, isDeleted);

            Tabl.ItemsSource = costTracking.GetData();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                int id = (int)((DataRowView)Tabl.SelectedItem)["CostID"];
                costTracking.DeleteQuery(id);

                Tabl.ItemsSource = costTracking.GetData();
            }
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;
                int id = (int)selectedRow["CostID"];
                int process = (int)Process.SelectedValue;
                int expenseType = (int)ExponceType.SelectedValue;
                decimal sum = decimal.Parse(Sum.Text);
                bool isDeleted = false;

                costTracking.UpdateQuery(process, expenseType, sum, isDeleted, id);

                Tabl.ItemsSource = costTracking.GetData();
            }
        }

        private void Tabl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tabl.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)Tabl.SelectedItem;

                // Пример: Обновление ComboBox для производства
                int process = (int)selectedRow["ProcessID"];
                Process.SelectedValue = process;

                // Пример: Обновление ComboBox для типа затрат
                int expenseType = (int)selectedRow["ExpenseTypeID"];
                ExponceType.SelectedValue = expenseType;

                // Пример: Обновление текстового поля для суммы
                decimal sum = (decimal)selectedRow["Amount"];
                Sum.Text = sum.ToString();
            }
        }

        private void Tabl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
