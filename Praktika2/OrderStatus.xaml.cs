using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class OrderStatus : Page
    {
        OrderStatusTableAdapter orderProducts = new OrderStatusTableAdapter();
        public OrderStatus()
        {
            InitializeComponent();
            Tabl.ItemsSource = orderProducts.GetData();
        }
    }
}
