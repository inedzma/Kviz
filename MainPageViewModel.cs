using System.Collections.ObjectModel;
using System.Threading.Tasks;



namespace Kviz
{
	public class MainPageViewModel
	{
		public ObservableCollection<Drzava> Drzave { get; set; }
		private readonly LocalDBService _dbService;

		public MainPageViewModel()
		{
			_dbService = new LocalDBService();
			Drzave = new ObservableCollection<Drzava>();

			Task.Run(async () => await InitializeAsync());
		}
		public async Task InitializeAsync()
		{
			await LoadDrzave();
		}

		private async Task LoadDrzave()
		{

			if (Drzave.Count > 0) return;

			Drzave.Clear();
			var drzave = await _dbService.GetAllDrzaveAsync();

			Console.WriteLine($"Broj drzava dodanih: {drzave.Count}");
			foreach (var drzava in drzave)
			{
				Drzave.Add(drzava);
				//if (Drzave.Count >= 196) break;
			}
		}
	}
}
