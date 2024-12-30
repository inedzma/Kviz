namespace Kviz
{
	public partial class MainPage : ContentPage
	{
		private MainPageViewModel viewModel;
		public MainPage()
		{
			InitializeComponent();
			viewModel= new MainPageViewModel();
			BindingContext = viewModel;

			viewModel.StartQuizEvent += OnStartQuiz;
			viewModel.StopQuizEvent += OnStopQuiz;

			
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
	}

}
