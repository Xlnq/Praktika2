using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class ExponseType : Page
    {
        ExpenseTypeTableAdapter expenseType = new ExpenseTypeTableAdapter();
        public ExponseType()
        {
            InitializeComponent();
            Tabl.ItemsSource = expenseType.GetData();
        }
    }
}
