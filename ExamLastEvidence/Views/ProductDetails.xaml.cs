using ExamLastEvidence.Services;
using ExamLastEvidence.ViewModels;

namespace ExamLastEvidence.Views;

[QueryProperty(nameof(ProductId), "id")]
public partial class ProductDetails : ContentPage
{
	private readonly ProductService productService;
	private ProductViewModel? product;
	
	public ProductDetails(ProductService productSerice)
	{
		InitializeComponent();
		this.productService = productSerice;
		

	}
	int _id;
	public int ProductId
	{
		get => _id;
		set
		{
			_id = value;
			OnPropertyChanged(nameof(ProductId));
			CallGetData();
		}
	}
	public async void CallGetData()
	{
		this.product = await GetProduct();
		SetBindings();
	}
	public async Task<ProductViewModel?> GetProduct()
	{
		var data = await this.productService.Get(this.ProductId);
		return data;
	}
	public void SetBindings()
	{
		Binding nameBinding = new Binding {  Source = this.product, Path="Name" };
		this.lblName.SetBinding(Label.TextProperty, nameBinding);
        //this.lbName.Text = this.product?.Name;
        Binding descBinding = new Binding { Source = this.product, Path = "Description" };
        this.lblDesc.SetBinding(Label.TextProperty, descBinding);
        Binding priceBinding = new Binding { Source = this.product, Path = "Price" };
        this.lblPrice.SetBinding(Label.TextProperty, priceBinding);
        Binding picBinding = new Binding { Source = this.product, Path = "RemotePictureUrl" };
        this.impPic.SetBinding(Image.SourceProperty, picBinding);
    }
}