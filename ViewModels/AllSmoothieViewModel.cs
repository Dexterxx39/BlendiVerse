namespace BlendiVerseApp.ViewModels
{
    [QueryProperty(nameof(FromSearch), nameof(FromSearch))]
    public partial class AllSmoothieViewModel : ObservableObject
    {
        private readonly SmoothieService _smoothieService;

        public AllSmoothieViewModel(SmoothieService smoothieService)
        {
            _smoothieService = smoothieService;
            Smoothies = new ObservableCollection<Smoothie>();
            LoadSmoothies();
        }

        public ObservableCollection<Smoothie> Smoothies { get; set; }

        [ObservableProperty]
        private bool _fromSearch;

        [ObservableProperty]
        private bool _searching;

        // Fetch all smoothies from Firestore and populate the list
        private async void LoadSmoothies()
        {
            var smoothies = await _smoothieService.GetAllSmoothiesAsync();
            foreach (var smoothie in smoothies)
            {
                Smoothies.Add(smoothie);
            }
        }

        [RelayCommand]
        private async Task SearchSmoothies(string searchTerm)
        {
            Smoothies.Clear();
            Searching = true;
            await Task.Delay(1000); // Optional delay for UI effect
            var smoothies = await _smoothieService.SearchSmoothiesAsync(searchTerm);
            foreach (var smoothie in smoothies)
            {
                Smoothies.Add(smoothie);
            }
            Searching = false;
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
