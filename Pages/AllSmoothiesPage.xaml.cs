namespace BlendiVerseApp.Pages;

public partial class AllSmoothiesPage : ContentPage
{
	private readonly AllSmoothieViewModel _allSmoothieViewModel;
	public AllSmoothiesPage(AllSmoothieViewModel allSmoothieViewModel)
	{
		InitializeComponent();
		_allSmoothieViewModel = allSmoothieViewModel;
		BindingContext = _allSmoothieViewModel;
	}

	protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_allSmoothieViewModel.FromSearch)
        {
            await Task.Delay(100);
            searchBar.Focus();
        }
    }

    void searchBar_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
		if(!string.IsNullOrWhiteSpace(e.OldTextValue)
            && string.IsNullOrWhiteSpace(e.NewTextValue))
			{
				_allSmoothieViewModel.SearchSmoothiesCommand.Execute(null);
			}
    }
}