using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class ProductQualityStatus : Page
    {
        ProductQualityStatusTableAdapter productQualityStatus = new ProductQualityStatusTableAdapter();
        public ProductQualityStatus()
        {
            InitializeComponent();
            Tabl.ItemsSource = productQualityStatus.GetData();
        }
    }
}
