using ExamLastEvidence.Views;

namespace ExamLastEvidence
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("ProductList", typeof(ProductListContenPage));
            Routing.RegisterRoute("ProductDetails", typeof(ProductDetails));
            Routing.RegisterRoute("Cart", typeof(CartPage));
        }
    }
}
