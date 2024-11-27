#if IOS
using CommunityToolkit.Maui.Behaviors;
using UIKit;
#endif

namespace BlendiVerseApp.Pages
{
    public partial class DetailPage : ContentPage
    {
        private readonly DetailsViewModel _detailsViewModel;

        public DetailPage(DetailsViewModel detailsViewModel)
        {
            _detailsViewModel = detailsViewModel;
            InitializeComponent();
            BindingContext = _detailsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
#if IOS
            // Adjust the bottom margin for iOS safe area
            var bottom = UIApplication.SharedApplication.Delegate.GetWindow().SafeAreaInsets.Bottom;
            bottomBox.Margin = new Thickness(-1, 0, -1, (bottom + 1) * -1);
#endif
        }

        // Navigation Back Button Handler
        async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..", animate: true);
        }

        // Apply status bar appearance on navigating from the page
        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            base.OnNavigatingFrom(args);

#if IOS
            Behaviors.Add(new StatusBarBehavior
            {
                StatusBarColor = Color.FromArgb("#5F6F52"),
                StatusBarStyle = CommunityToolkit.Maui.Core.StatusBarStyle.LightContent
            });
#endif
        }

        // Command logic for increasing the cart quantity
        private void OnAddToCart()
        {
            _detailsViewModel.Smoothie.CartQuantity++;
            OnPropertyChanged(nameof(_detailsViewModel.Smoothie.CartQuantity));
        }

        // Command logic for decreasing the cart quantity
        private void OnRemoveFromCart()
        {
            if (_detailsViewModel.Smoothie.CartQuantity > 0)
            {
                _detailsViewModel.Smoothie.CartQuantity--;
                OnPropertyChanged(nameof(_detailsViewModel.Smoothie.CartQuantity));
            }
        }
    }
}
