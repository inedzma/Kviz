using System.Collections.ObjectModel;

namespace Kviz;

public partial class RezultatiInfo
{
	private readonly LocalDBService dBService;
	public ObservableCollection<Rezultat> Rezultati { get; set; }
	public string Email { get; set; }	
	public RezultatiInfo(string initialTab = "Rezultati")
	{
		InitializeComponent();
		dBService = App._dbService;
		Rezultati = new ObservableCollection<Rezultat>();
		Task.Run(UcitajRezultate);
		BindingContext = this;
		Email = App.Email;
		// Prebaci na odgovarajući tab
		if (initialTab == "Info")
		{
			RezultatiSection.IsVisible = false;
			InfoSection.IsVisible = true;
		}
		else
		{
			InfoSection.IsVisible = false;
			RezultatiSection.IsVisible = true; // Rezultati tab
		}
		NavigationPage.SetHasNavigationBar(this, false);
	}
	private async Task UcitajRezultate()
	{
		var rezultati = await dBService.GetRezultateEmail(Email);
		Rezultati.Clear();
		for(int r = rezultati.Count()-1; r>=0; r--)
		{
			Rezultati.Add(rezultati[r]);
		}
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}
