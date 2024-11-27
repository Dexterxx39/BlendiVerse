using System.Collections.ObjectModel;
using BlendiVerseApp.Services;
using BlendiVerseApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlendiVerseApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly SmoothieService _smoothieService;

        // Constructor
        public HomeViewModel(SmoothieService smoothieService)
        {
            _smoothieService = smoothieService;
            LoadPopularSmoothiesAsync(); // Call the async method to fetch smoothies
        }

        public ObservableCollection<Smoothie> Smoothies { get; set; } = new ObservableCollection<Smoothie>();

        // Fetch popular smoothies asynchronously
        private async void LoadPopularSmoothiesAsync()
        {
            var smoothies = await _smoothieService.GetPopularSmoothiesAsync();  // Ensure the method is async
            Smoothies.Clear();  // Clear current collection
            foreach (var smoothie in smoothies)  // Add new smoothies to the collection
            {
                Smoothies.Add(smoothie);
            }
        }

        [RelayCommand]
        private async Task GoToAllSmoothiesPage(bool fromSearch = false)
        {
            var parameters = new Dictionary<string, object>
            {
                [nameof(AllSmoothieViewModel.FromSearch)] = fromSearch
            };
            await Shell.Current.GoToAsync(nameof(AllSmoothiesPage), animate: true, parameters);
        }

        [RelayCommand]
        private async Task GoToDetailsPage(Smoothie smoothie) 
        {
            var parameters = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Smoothie)] = smoothie
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), animate: true, parameters);
        }
    }
}
