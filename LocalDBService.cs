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

			Console.WriteLine("Podaci su uspešno dodani iz JSON-a.");
		}

		public async Task ClearDrzavaTableAsync()
		{
			await _connection.DeleteAllAsync<Drzava>();
		}






	}
}
