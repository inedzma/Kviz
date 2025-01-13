namespace Kviz
{
	public partial class App : Application
	{
		public static LocalDBService _dbService { get; } = new LocalDBService();
		public App(MainPage mainPage)
		{
			InitializeComponent();

			MainPage = mainPage;

			InitializeDatabase();

			MainPage = new NavigationPage(new LoginPage());
		}


		private async void InitializeDatabase()
		{

			// Tačan naziv resursa
			var resourceName = "Kviz.Resources.Raw.drzave.json";


			// Seed baza iz JSON-a
			await _dbService.SeedFromJsonAsync(resourceName);
		}


	}
}
