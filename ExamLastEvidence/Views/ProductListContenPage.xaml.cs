using ExamLastEvidence.Services;
using ExamLastEvidence.ViewModels;
using System;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExamLastEvidence.Views;

public partial class ProductListContenPage : ContentPage
{
    private readonly ProductCollection productCollection;
    private readonly ProductCollection _model;// = new(); 
    private readonly ProductService productService;
    private  List<CartItemViewModel> cart;
    public ProductListContenPage(ProductCollection productCollection, ProductService productService)
    {
        InitializeComponent();
        this.productCollection = productCollection;
        this.productService = productService;
        CallGetProducts();
        this.BindingContext = this.productCollection;
        this._model = new();
        cart = new List<CartItemViewModel>();
        ReadCart();
        //this.cartText.Text = "4 items";
    }
   
    private async void CallGetProducts()
    {
        await GetProducts();
        foreach (var item in productCollection) {
            this._model.Add(item);
        }


    }
    private async Task GetProducts()
    {

        var data = await this.productService.Get();
        if (data == null) { return; }
        foreach (var item in data)
        {

            productCollection.Add(item);
        }
       
    }

    private async void ProductTapItem_Tapped(object sender, TappedEventArgs e)
    {
        var tappedItem = sender as Frame;
        var data = tappedItem?.BindingContext as ProductViewModel;
        if(data == null) { return; }
        await Shell.Current.GoToAsync($"ProductDetails?id={data.Id}");
    }

    private void AddToCart_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var id = int.Parse(btn?.CommandParameter.ToString() ?? "0");
        var product = this.productCollection.FirstOrDefault(x => x.Id == id);
        var item = cart.Find(x => x.ProductId == id);
        if(item == null) {
            cart.Add(new CartItemViewModel {  ProductId=id, Quantity = 1, ProductName=product?.Name ?? "", Price=product?.Price ?? 0.0M });
        }
        else
        {
            item.Quantity += 1;
        }
        WriteCart();
        ReadCart();
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
                
                cart = data;
                //cart.Clear();
                var count =
                cart.Sum(x => x.Quantity);
                this.cartText.Text = $"{count} items in cart";
            }
        }
    }
    public void WriteCart()
    {
        string cartString = JsonSerializer.Serialize(cart);
        File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, "cart.json"), cartString);
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        var searchBar = (SearchBar)sender;
        string s = searchBar?.Text ?? "";
       
        var data = this._model.Where(x => x.Name.StartsWith(s.ToLower(), StringComparison.OrdinalIgnoreCase)).ToList();
        this.productCollection.ClearList();
        foreach (var item in data)
        {
            this.productCollection.Add(item);
        }
        OnPropertyChanged();
    }

   

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Cart");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        this.productCollection.ClearList();
        foreach (var item in this._model)
        {
            this.productCollection.Add(item);
        }
        OnPropertyChanged();
        this.searchBar.Text = string.Empty;
        if (searchBar.IsSoftInputShowing())
            await this.searchBar.HideSoftInputAsync(System.Threading.CancellationToken.None);
        
    }
}