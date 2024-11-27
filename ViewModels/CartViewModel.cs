using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace BlendiVerseApp.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        public event EventHandler<Smoothie> CartItemRemoved;
        public event EventHandler<Smoothie> CartItemUpdated;
        public event EventHandler CartCleared;

        // Observable collection of smoothies in the cart
        public ObservableCollection<Smoothie> Items { get; set; } = new();

        [ObservableProperty]
        private double _totalAmount;

        // Recalculate the total amount whenever items change
        private void RecalculateTotalAmount() => TotalAmount = Items.Sum(i => i.Amount);

        [RelayCommand]
        private void UpdateCartItem(Smoothie smoothie)
        {
            var item = Items.FirstOrDefault(i => i.Name == smoothie.Name);
            if (item != null)
            {
                // Update the cart quantity (this will automatically update Amount)
                item.CartQuantity = smoothie.CartQuantity;
            }
            else
            {
                // If smoothie doesn't exist in the cart, add it
                Items.Add(smoothie.Clone());
            }

            // Recalculate the total amount
            RecalculateTotalAmount();
        }

        [RelayCommand]
        private async void RemoveCartItem(string name)
        {
            var item = Items.FirstOrDefault(i => i.Name == name);
            if (item != null)
            {
                Items.Remove(item);
                RecalculateTotalAmount();

                // Raise event for item removal
                CartItemRemoved?.Invoke(this, item);

                // Snackbar to undo removal
                var snackbarOptions = new SnackbarOptions
                {
                    CornerRadius = 10,
                    BackgroundColor = Color.FromArgb("#D9DDDC"),
                    Font = Microsoft.Maui.Font.OfSize("Lexend", 14),
                    ActionButtonFont = Microsoft.Maui.Font.OfSize("LexendBold", 16)
                };

                var snackbar = Snackbar.Make($"'{item.Name}' removed from Cart",
                    () =>
                    {
                        Items.Add(item);
                        RecalculateTotalAmount();
                        CartItemUpdated?.Invoke(this, item);
                    }, "Undo", TimeSpan.FromSeconds(5), snackbarOptions);

                await snackbar.Show();
            }
        }

        [RelayCommand]
        private async Task ClearCart()
        {
            if (await Shell.Current.DisplayAlert("Confirm Clear Cart?", "Do you really want to clear the cart items?", "Yes", "No"))
            {
                Items.Clear();
                RecalculateTotalAmount();
                CartCleared?.Invoke(this, EventArgs.Empty);

                await Toast.Make("Cart Cleared", ToastDuration.Short).Show();
            }
        }

        [RelayCommand]
        private async Task PlaceOrder()
        {
            Items.Clear();
            CartCleared?.Invoke(this, EventArgs.Empty);
            RecalculateTotalAmount();
            await Shell.Current.GoToAsync(nameof(CheckoutPage), animate: true);
        }
    }
}
