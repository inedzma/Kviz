using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;



namespace Kviz
{

	public class MainPageViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Drzava> Drzave { get; set; }
		

		

		private bool _isEvropaSelected;
		public bool isEvropaSelected
		{
			get => _isEvropaSelected;
			set
			{
				if (_isEvropaSelected != value)
				{
					_isEvropaSelected = value;
					OnPropertyChanged(nameof(isEvropaSelected));
					if (value) FiltrirajDrzave("Evropa");
				}
			}
		}

		private bool _isAzijaSelected;
		public bool isAzijaSelected
		{
			get => _isAzijaSelected;
			set
			{
				if (_isAzijaSelected != value)
				{
					_isAzijaSelected = value;
					OnPropertyChanged(nameof(isAzijaSelected));
					if (value) FiltrirajDrzave("Azija"); 
				}
			}
		}

		private bool _isAfrikaSelected;
		public bool isAfrikaSelected
		{
			get => _isAfrikaSelected;
			set
			{
				if (_isAfrikaSelected != value)
				{
					_isAfrikaSelected = value;
					OnPropertyChanged(nameof(isAfrikaSelected));
					if (value) FiltrirajDrzave("Afrika");
				}
			}
		}

		private bool _isAustralijaSelected;
		public bool isAustralijaSelected
		{
			get => _isAustralijaSelected;
			set
			{
				if (_isAustralijaSelected != value)
				{
					_isAustralijaSelected = value;
					OnPropertyChanged(nameof(isAustralijaSelected));
					if (value) FiltrirajDrzave("Australija i Okeanija"); 
				}
			}
		}

		private bool _isSAmerikaSelected;
		public bool isSAmerikaSelected
		{
			get => _isSAmerikaSelected;
			set
			{
				if (_isSAmerikaSelected != value)
				{
					_isSAmerikaSelected = value;
					OnPropertyChanged(nameof(isSAmerikaSelected));
					if (value) FiltrirajDrzave("Sjeverna Amerika");
				}
			}
		}

		private bool _isJAmerikaSelected;
		public bool isJAmerikaSelected
		{
			get => _isJAmerikaSelected;
			set
			{
				if (_isJAmerikaSelected != value)
				{
					_isJAmerikaSelected = value;
					OnPropertyChanged(nameof(isJAmerikaSelected));
					if (value) FiltrirajDrzave("Južna Amerika"); 				}
			}
		}

		private bool _isSvijetSelected;
		public bool isSvijetSelected
		{
			get => _isSvijetSelected;
			set
			{
				if (_isSvijetSelected != value)
				{
					_isSvijetSelected = value;
					OnPropertyChanged(nameof(isSvijetSelected));
					if (value) FiltrirajDrzave("Svijet"); 
				}
			}
		}

		private int brojPitanja;
		private bool _isSvaSelected;
		public bool isSvaSelected
		{
			get => _isSvaSelected;
			set
			{
				if (_isSvaSelected != value)
				{
					_isSvaSelected = value;
					OnPropertyChanged(nameof(isSvaSelected));
					brojPitanja = FiltriraneDrzave.Count();
				}
			}
		}
		private bool _is10Selected;
		public bool is10Selected
		{
			get => _is10Selected;
			set
			{
				if (_is10Selected != value)
				{
					_is10Selected = value;
					OnPropertyChanged(nameof(is10Selected));
					brojPitanja = 10;
				}
			}
		}

		private bool _isZastaveSelected;
		public bool isZastaveSelected
		{
			get => _isZastaveSelected;
			set
			{
				if( _isZastaveSelected != value)
				{
					_isZastaveSelected = value;
					OnPropertyChanged(nameof(isZastaveSelected));
				}
			}
		}

		private bool _isGradoviSelected;
		public bool isGradoviSelected
		{
			get => _isGradoviSelected;
			set
			{
				if ( _isGradoviSelected != value)
				{
					_isGradoviSelected = value;
					OnPropertyChanged(nameof( isGradoviSelected));
				}
			}
		}



		private Drzava _trenutnoPitanje;
		public Drzava? TrenutnoPitanje
		{
			get => _trenutnoPitanje;
			set
			{
				_trenutnoPitanje = value;
				OnPropertyChanged(nameof(TrenutnoPitanje));
			}
		}

		private int _trenutnoPitanjeIndex;

		public Command ZapocniKvizCommand { get; }

		public Command<object> SljedecePitanjeCommand { get; }

		public ObservableCollection<Drzava> FiltriraneDrzave { get; set; }

		public MainPageViewModel()
		{
			Drzave = new ObservableCollection<Drzava>();
			FiltriraneDrzave = new ObservableCollection<Drzava>();
			Task.Run(async () => await InitializeAsync());
			ZapocniKvizCommand = new Command(ZapocniKviz);
			SljedecePitanjeCommand = new Command<object>(SljedecePitanje);
		}
		public async Task InitializeAsync()
		{
			await LoadDrzave();
		}

		private async Task LoadDrzave()
		{

			if (Drzave.Count > 0) return;

			Drzave.Clear();
			var drzave = await App._dbService.GetAllDrzaveAsync();

			Console.WriteLine($"Broj drzava dodanih: {drzave.Count}");
			foreach (var drzava in drzave)
			{
				Drzave.Add(drzava);
				if (Drzave.Count >= 196) break;
			}
		}
		IEnumerable<Drzava> drzaveZaFiltraciju;

		public void FiltrirajDrzave(string kontinent)
		{
			FiltriraneDrzave.Clear();

			var random = new Random();

			
			if (string.IsNullOrEmpty(kontinent) || kontinent == "Svijet")
			{
				drzaveZaFiltraciju = Drzave;
			}
			else
			{
				drzaveZaFiltraciju = Drzave.Where(d => d.Kontinent == kontinent);
			}

			foreach(var drzava in drzaveZaFiltraciju.OrderBy(x => random.Next()))
			{
				FiltriraneDrzave.Add(drzava);
			}

			Console.WriteLine($"Broj filtriranih drzava: {FiltriraneDrzave.Count()}\n\n\n");
		}

		public event EventHandler StartQuizEvent;
		public event EventHandler StopQuizEvent;
		
		public ObservableCollection<string> odgovori { get; set; } = new ObservableCollection<string>();
		public string TacanOdgovor;

		private int _tacniOdgovori;
		public int TacniOdgovori
		{
			get => _tacniOdgovori;
			private set
			{
				if(_tacniOdgovori != value)
				{
					_tacniOdgovori = value;
					OnPropertyChanged(nameof(TacniOdgovori));
				}
			}
		}

		public void NetacniOdgovori(Drzava pitanje)
		{
			var random = new Random();
			odgovori.Clear();

			if (isGradoviSelected)
			{
				odgovori.Add(pitanje.GlavniGrad);

				var moguciOdgovori = FiltriraneDrzave.Where(d => d.GlavniGrad != odgovori[0]).OrderBy(x => random.Next()).Take(3).Select(d => d.GlavniGrad).ToList();
				foreach (var o in moguciOdgovori)
				{
					odgovori.Add(o);
				}
			}
			else
			{
				odgovori.Add(pitanje.Naziv);

				var moguciOdgovori = FiltriraneDrzave.Where(d => d.Naziv != odgovori[0]).OrderBy(x => random.Next()).Take(3).Select(d => d.Naziv).ToList();
				foreach (var o in moguciOdgovori)
				{
					odgovori.Add(o);
				}
			}
			var shuffled = odgovori.OrderBy(x => random.Next()).ToList();
			odgovori.Clear();
			foreach(var o in shuffled)
			{
				odgovori.Add(o);
			}
			 

		}
		public void ZapocniKviz()
		{
			if (FiltriraneDrzave.Count == 0)
			{
				TrenutnoPitanje = null;
				return;
			}

			_trenutnoPitanjeIndex = 0;
			TacniOdgovori = 0;
			
			TrenutnoPitanje = FiltriraneDrzave[_trenutnoPitanjeIndex];
			TacanOdgovor = isGradoviSelected ? TrenutnoPitanje.GlavniGrad : TrenutnoPitanje.Naziv;
			NetacniOdgovori(TrenutnoPitanje);
			Console.WriteLine($"Broj filtriranih drzava: {FiltriraneDrzave.Count()}\n\n\n");

			StartQuizEvent?.Invoke(this, EventArgs.Empty);
			
		}

		public void SljedecePitanje(object parameter)
		{
			if (TacanOdgovor == parameter.ToString())
			{
				TacniOdgovori++;
				Console.WriteLine(TacniOdgovori);
			}
			Console.WriteLine($"Tacan odgovor: {TacanOdgovor}, Proslijeđeni parametar: {parameter.ToString()}");
			if(_trenutnoPitanjeIndex < brojPitanja-1)
			{
				_trenutnoPitanjeIndex++;
				TrenutnoPitanje = FiltriraneDrzave[_trenutnoPitanjeIndex];
				TacanOdgovor = isGradoviSelected ? TrenutnoPitanje.GlavniGrad : TrenutnoPitanje.Naziv;
				NetacniOdgovori(TrenutnoPitanje);
				
			}
			else if(_trenutnoPitanjeIndex == brojPitanja-1)
			{
				StopQuizEvent?.Invoke(this, EventArgs.Empty);
				
			}
			
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
