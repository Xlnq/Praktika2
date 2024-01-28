using System.Windows.Controls;
using Praktika2.PraktikaDataSetTableAdapters;

namespace Praktika2
{
    public partial class ProductCategory : Page
    {
        ProductCategoryTableAdapter productCategory = new ProductCategoryTableAdapter();
        public ProductCategory()
        {
            InitializeComponent();
            Tabl.ItemsSource = productCategory.GetData();
        }
    }
}
