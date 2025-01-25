using Microsoft.Maui;

namespace Kviz;

public partial class RegistrationPage : ContentPage
{
	private readonly LocalDBService _dbService;
	public RegistrationPage()
	{
		_dbService = App._dbService;
		InitializeComponent();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		Korisnik korisnik = new Korisnik()
		{
			Name = "Mujo",
			Surname = "Mujic",
			Email = email.Text,
			Password = lozinka.Text,

		};

		if (string.IsNullOrEmpty(korisnik.Email))
		{
			await DisplayAlert("Greska!", "Molimo unesite vaš email!", "OK");
			email.PlaceholderColor = Colors.Red;
			emailLabel.TextColor = Colors.Red;
			return;
		}
		else if (string.IsNullOrEmpty(korisnik.Password))
		{
			await DisplayAlert("Greska!", "Molimo unesite lozinku!", "OK");
			lozinka.PlaceholderColor = Colors.Red;
			lozinkaLabel.TextColor = Colors.Red;
			return;
		}
		var Email = await App._dbService.GetKorisnikAsync(korisnik.Email);
		if (Email != null)
		{
			await DisplayAlert("Greska!", "Unešeno ime je već uneseno od strane drugog korisnika! Molimo  unesite novo ime.", "OK");
			email.PlaceholderColor = Colors.Red;
			emailLabel.TextColor = Colors.Red;
			return;
		}
		
		await App._dbService.SaveKorisnikAsync(korisnik);
		await App._dbService.AppendUserToJsonAsync(korisnik);

		App.Email = email.Text;

		await Navigation.PushAsync(new MainPage());
		
	}
}