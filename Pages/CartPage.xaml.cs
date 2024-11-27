namespace BlendiVerseApp.Pages;

public partial class CartPage : ContentPage
{
    private readonly CartViewModel _cartViewModel;
    public CartPage(CartViewModel cartViewModel)
    {
        _cartViewModel = cartViewModel;
        InitializeComponent();
        BindingContext = _cartViewModel;
    }

	async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AllSmoothiesPage));
    }
}