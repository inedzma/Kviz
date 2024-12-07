namespace Kviz
{
	public partial class App : Application
	{
		public App(MainPage mainPage)
		{
			InitializeComponent();

			MainPage = mainPage;

			InitializeDatabase();

			var resources = typeof(App).Assembly.GetManifestResourceNames();
			foreach (var resource in resources)
			{
				Console.WriteLine("helooo" + resource);
			}

			TestDatabase();

		}

		private async void TestDatabase()
		{
			var dbService = new LocalDBService();
			var drzave = await dbService.GetAllDrzaveAsync();
			foreach (var drzava in drzave)
			{
				Console.WriteLine($"Naziv: {drzava.Naziv}, Glavni grad: {drzava.GlavniGrad}");
			}
		}

		private async void InitializeDatabase()
		{
			var dbService = new LocalDBService();

			// Dohvati Assembly
			var assembly = typeof(App).Assembly;

			// Tačan naziv resursa
			var resourceName = "Kviz.Resources.Raw.drzave.json";

			await dbService.ClearDrzavaTableAsync();

			// Seed baza iz JSON-a
			await dbService.SeedFromJsonAsync(resourceName);
		}


	}
}
