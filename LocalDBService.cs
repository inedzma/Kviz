using SQLite;
using System.Reflection;
using System.Text.Json;
namespace Kviz
{
	public class LocalDBService
	{
		private const string DB_NAME = "lokalna_baza";
		private readonly SQLiteAsyncConnection _connection;
		public LocalDBService()
		{
			_connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
			_connection.CreateTableAsync<Drzava>().Wait();
			_connection.CreateTableAsync<Pitanje>().Wait();
			_connection.CreateTableAsync<Korisnik>().Wait();
			_connection.CreateTableAsync<Rezultat>().Wait();
		}

		public Task<List<Drzava>> GetAllDrzaveAsync()
		{
			return _connection.Table<Drzava>().ToListAsync();
		}

		public Task<int> SaveDrzavaAsync(Drzava drzava)
		{
			return _connection.InsertAsync(drzava);
		}

		public Task<int> DeleteDrzavaAsync(int id)
		{
			return _connection.DeleteAsync<Drzava>(id);
		}

		public Task<List<Pitanje>> GetPitanjaForDrzavaAsync(int drzavaId)
		{
			return _connection.Table<Pitanje>().Where(p => p.TacanOdgovorId == drzavaId).ToListAsync();
		}

		public Task<int> SavePitanjeAsync(Pitanje pitanje)
		{
			return _connection.InsertAsync(pitanje);
		}

		public Task<int> DeletePitanjeAsync(int id)
		{
			return _connection.DeleteAsync<Pitanje>(id);
		}

		public Task<List<Korisnik>> GetAllKorisnici()
		{
			return _connection.Table<Korisnik>().ToListAsync();
		}

		public Task<int> SaveKorisnikAsync(Korisnik korisnik)
		{
			return _connection.InsertAsync(korisnik);
		}

		public Task<int> DeleteKorisnikAsync(int id)
		{
			return _connection.DeleteAsync<Korisnik>(id);
		}

		public Task<Korisnik> GetKorisnikAsync(string email)
		{
			return _connection.Table<Korisnik>().Where(k => k.Email == email).FirstOrDefaultAsync();
		}

		/*public Task<bool> DoesKorisnikExist(string email)
		{
			return _connection.Table<Korisnik>().Where(k => k.Email == email).FirstOrDefaultAsync();
		}*/

		public Task<List<Rezultat>> GetSveRezulate()
		{
			return _connection.Table<Rezultat>().ToListAsync();
		}

		public Task<int> SaveRezultatAsync(Rezultat rezultat)
		{
			return _connection.InsertAsync(rezultat);
		}

		public Task<List<Rezultat>> GetRezultateKontinent(string continent)
		{
			return _connection.Table<Rezultat>().Where(r => r.Kontinent == continent).ToListAsync();
		}

		public Task<int> DeleteRezultatAsync(int id)
		{
			return _connection.DeleteAsync<Rezultat>(id);
		}

		public async Task SeedFromJsonAsync(string resourceName)
		{
			// Proverite da li već postoje podaci u tabeli
			var existingCount = await _connection.Table<Drzava>().CountAsync();
			if (existingCount > 0)
			{
				Console.WriteLine("Podaci već postoje u bazi, preskačem učitavanje iz JSON-a.");
				return;
			}

			// Dohvati JSON resurs
			using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
			if (stream == null)
			{
				throw new InvalidOperationException($"Resource '{resourceName}' not found. Ensure it is marked as an Embedded Resource.");
			}

			using var reader = new StreamReader(stream);
			var jsonContent = await reader.ReadToEndAsync();

			// Parsiranje JSON-a
			var drzave = JsonSerializer.Deserialize<List<Drzava>>(jsonContent);

			if (drzave != null)
			{
				// Unesi države u bazu
				foreach (var drzava in drzave)
				{
					await SaveDrzavaAsync(drzava);
				}
			}

			Console.WriteLine("Podaci su uspješno dodani iz JSON-a.");
		}

		public async Task ClearDrzavaTableAsync()
		{
			await _connection.DeleteAllAsync<Drzava>();
		}

		public async Task AppendUserToJsonAsync(Korisnik noviKorisnik)
		{
			string jsonFilePath = Path.Combine(FileSystem.AppDataDirectory, "korisnici.json");

			List<Korisnik> korisnici = new List<Korisnik>();

			if (File.Exists(jsonFilePath))
			{
				var jsonContent = await File.ReadAllTextAsync(jsonFilePath);

				korisnici = JsonSerializer.Deserialize<List<Korisnik>>(jsonContent) ?? new List<Korisnik>();
			}

			korisnici.Add(noviKorisnik);

			var updatedJsonContent = JsonSerializer.Serialize(korisnici, new JsonSerializerOptions { WriteIndented = true });
			await File.WriteAllTextAsync(jsonFilePath, updatedJsonContent);
		}

		public async Task LoadUsersFromJsonAsync()
		{
			string jsonFile = Path.Combine(FileSystem.AppDataDirectory, "korisnici.json");

			if (!File.Exists(jsonFile))
			{
				return;
			}
			var jsonContent = await File.ReadAllTextAsync(jsonFile);
			var korisnici = JsonSerializer.Deserialize<List<Korisnik>>(jsonContent);
			if(korisnici == null || korisnici.Count == 0)
			{
				return;	
			}
			var existingCount = await _connection.Table<Korisnik>().CountAsync();
			if(existingCount > 0)
			{
				return;
			}
			foreach (var korisnik in korisnici)
			{
				await SaveKorisnikAsync(korisnik);
			}
		}







	}
}
