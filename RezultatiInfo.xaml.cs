using System.Collections.ObjectModel;

namespace Kviz;

public partial class RezultatiInfo : TabbedPage
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
			CurrentPage = Children[1]; // Info tab
		}
		else
		{
			CurrentPage = Children[0]; // Rezultati tab
		}
	}
	private async Task UcitajRezultate()
	{
		var rezultati = await dBService.GetRezultateEmail(Email);
		Rezultati.Clear();
		foreach(var r in rezultati)
		{
			Rezultati.Add(r);
			Console.WriteLine("Dodano", r.TacniOdgovori);
		}
	}
}
