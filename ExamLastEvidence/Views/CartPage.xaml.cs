using ExamLastEvidence.Services;
using ExamLastEvidence.ViewModels;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace ExamLastEvidence.Views;

public partial class CartPage : ContentPage
{
    private ObservableCollection<CartItemViewModel> cart;
    
    public CartPage()
	{
		InitializeComponent();
       
        this.cart = new ObservableCollection<CartItemViewModel>();
        this.BindingContext = this.cart;
        ReadCart();
	}
    
    public void WriteCart()
    {
        string cartString = JsonSerializer.Serialize(cart);
        File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, "cart.json"), cartString);
    }
    private void ReadCart()
    {
        if (File.Exists(Path.Combine(FileSystem.AppDataDirectory, "cart.json")))
        {
            var cartString = File.ReadAllText(Path.Combine(FileSystem.AppDataDirectory, "cart.json"));
            var data = JsonSerializer.Deserialize<List<CartItemViewModel>>(cartString);
            if (data == null)
            {

            }
            else
            {
                foreach (var item in data)
                {
                    this.cart.Add(item);
                }
               
                
            }
        }
    }
}