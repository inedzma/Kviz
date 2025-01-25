namespace Kviz
{
	public partial class MainPage : ContentPage
	{

		private MainPageViewModel viewModel;
		public string Email { get; set; }
		public MainPage()
		{
			InitializeComponent();
			viewModel= new MainPageViewModel();
			BindingContext = viewModel;

			viewModel.StartQuizEvent += OnStartQuiz;
			viewModel.StopQuizEvent += OnStopQuiz;

			NavigationPage.SetHasNavigationBar(this, false);
		}

		private void OnStartQuiz(object sender, EventArgs e)
		{
			FormatSelectionPage.IsVisible = false;
			pitanje.IsVisible = true;
		}

		private void OnStopQuiz(object sender, EventArgs e)
		{
			pitanje.IsVisible = false;
			kraj.IsVisible = true;
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			StartPage.IsVisible = false;
			ThemeSelectionPage.IsVisible = true;
		}

		protected override void OnDisappearing()
		{
			var viewModel = BindingContext as MainPageViewModel;
			if(viewModel!= null)
			{
				viewModel.StartQuizEvent -= OnStartQuiz;
				viewModel.StopQuizEvent -= OnStopQuiz;
			}
			base.OnDisappearing();
		}

		private void Button_Clicked_1(object sender, EventArgs e)
		{
			ThemeSelectionPage.IsVisible = false;
			ContinentSelectionPage.IsVisible = true;
		}

		private void Button_Clicked_2(object sender, EventArgs e)
		{
			ContinentSelectionPage.IsVisible = false;
			FormatSelectionPage.IsVisible = true;
		}

		private void Button_Clicked_3(object sender, EventArgs e)
		{
			kraj.IsVisible =false;
			StartPage.IsVisible=true;
			
		}
        private void Button_Clicked_5(object sender, EventArgs e)
        {

			FormatSelectionPage.IsVisible = false;
			ContinentSelectionPage.IsVisible = true;


        }

        private void Frame_Tapped(object sender, EventArgs e)
		{
			if (sender is Frame frame && frame.BindingContext is MainPageViewModel viewModel)
			{
				// Dohvati prosleđeni CommandParameter (kategoriju)
				var tapGestureRecognizer = (TapGestureRecognizer)frame.GestureRecognizers[0];
				var category = tapGestureRecognizer.CommandParameter.ToString();

				// Odaberi kategoriju
				viewModel.SelectCategory(category);

				// Pozovi funkcionalnost dugmeta
				Button_Clicked_2(sender, e);
			}
		}

        private void Frame_Tapped3(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is MainPageViewModel viewModel)
            {
                // Dohvati prosleđeni CommandParameter (kategoriju)
                var tapGestureRecognizer = (TapGestureRecognizer)frame.GestureRecognizers[0];
                var category = tapGestureRecognizer.CommandParameter.ToString();

                // Odaberi kategoriju
                viewModel.SelectCategory(category);

                // Pozovi funkcionalnost dugmeta
                OnStartQuiz(sender, e);

				viewModel.ZapocniKviz();
            }
        }


        private void Frame_Tapped2(object sender, EventArgs e)
		{
			if (sender is Frame frame && frame.BindingContext is MainPageViewModel viewModel)
			{
				// Dohvati prosleđeni CommandParameter (kategoriju)
				var tapGestureRecognizer = (TapGestureRecognizer)frame.GestureRecognizers[0];
				var category = tapGestureRecognizer.CommandParameter.ToString();

				// Odaberi kategoriju
				viewModel.SelectCategory(category);

				// Pozovi funkcionalnost dugmeta
				Button_Clicked_1(sender, e);
			}
		}

		private void Button_Clicked_4(object sender, EventArgs e)
		{
			// Proveri da li je sender dugme
			if (sender is Button button)
			{
				// Dohvati CommandParameter
				var commandParameter = button.CommandParameter as string;

				// Proveri vrednost i izvrši logiku
				if (commandParameter == "kontinent")
				{
					ContinentSelectionPage.IsVisible = false;
					StartPage.IsVisible = true;
					
				}
				else if(commandParameter == "zastave")
				{
					ThemeSelectionPage.IsVisible = false;
					StartPage.IsVisible = true;
				}
			}
		}

		private void ShowResults(object sender, EventArgs e)
		{
			Navigation.PushAsync(new RezultatiInfo("Rezultati"));
		}

		private void ShowInfo(object sender, EventArgs e)
		{
			Navigation.PushAsync(new RezultatiInfo("Info"));
		}



	}

}


