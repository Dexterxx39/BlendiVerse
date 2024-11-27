using System;

namespace BlendiVerseApp.ViewModels
{
    [QueryProperty(nameof(Smoothie), nameof(Smoothie))]
    public partial class DetailsViewModel : ObservableObject, IDisposable
    {
        private readonly CartViewModel _cartViewModel;
        public DetailsViewModel(CartViewModel cartViewModel)
        {
            _cartViewModel = cartViewModel;

            _cartViewModel.CartCleared += OnCartCleared;
            _cartViewModel.CartItemRemoved += OnCartItemRemoved;
            _cartViewModel.CartItemUpdated += OnCartItemUpdated;
        }

        private void OnCartCleared(object? _, EventArgs e) => Smoothie.CartQuantity = 0;
        private void OnCartItemRemoved(object? _, Smoothie s) => OnCartItemChanged(s, 0);
        private void OnCartItemUpdated(object? _, Smoothie s) => OnCartItemChanged(s, s.CartQuantity);
        private void OnCartItemChanged(Smoothie s, int quantity)
        {
            if (s.Name == Smoothie.Name)
            {
                Smoothie.CartQuantity = quantity;
            }
        }

        [ObservableProperty]
        private Smoothie _smoothie;

        [RelayCommand]
        private void AddToCart()
        {
            Smoothie.CartQuantity++;
            _cartViewModel.UpdateCartItemCommand.Execute(Smoothie);
        }

        [RelayCommand]
        private void RemoveFromCart()
        {
            if (Smoothie.CartQuantity > 0)
                Smoothie.CartQuantity--;
            _cartViewModel.UpdateCartItemCommand.Execute(Smoothie);
        }

        [RelayCommand]
        private async Task ViewCart()
{
    if (Smoothie.CartQuantity > 0)
    {
        await Shell.Current.GoToAsync(nameof(CartPage), animate: true);
    }
    else
    {
        ShowSnackBar("Please select the quantity using the plus (+) button");
    }
}

private void ShowSnackBar(string message)
{
    var snackbarOptions = new SnackbarOptions
    {
        BackgroundColor = Color.FromArgb("#D9DDDC"),
        CornerRadius = 10,
        Font = Microsoft.Maui.Font.OfSize("Lexend", 16),
        TextColor = Colors.Black
    };

    var snackbar = Snackbar.Make(
        message,
        null, // Action text (if any)
        "Dismiss", // Dismiss button text
        TimeSpan.FromSeconds(3), // Duration
        snackbarOptions
    );

    snackbar.Show();
}

        public void Dispose()
        {
            _cartViewModel.CartCleared -= OnCartCleared;
            _cartViewModel.CartItemRemoved -= OnCartItemRemoved;
            _cartViewModel.CartItemUpdated -= OnCartItemUpdated;
        }
    }
}