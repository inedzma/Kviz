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
			Name = ime.Text,
			Surname = prezime.Text,
			Email = email.Text,
			Password = lozinka.Text,

		};
		if (string.IsNullOrEmpty(korisnik.Name))
		{
			await DisplayAlert("Greska!", "Molimo unesite vaše ime!", "OK");
			ime.PlaceholderColor = Colors.Red;
			imeLabel.TextColor = Colors.Red;
			return;
		}
		else if (string.IsNullOrEmpty(korisnik.Surname))
		{
			await DisplayAlert("Greska!", "Molimo unesite vaše prezime!", "OK");
			ime.PlaceholderColor = Colors.Red;
			imeLabel.TextColor = Colors.Red;
			return;
		}
		else if (string.IsNullOrEmpty(korisnik.Email))
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
		else if (string.IsNullOrEmpty(potvrdiLozinku.Text))
		{
			await DisplayAlert("Greska!", "Molimo potvrdite lozinku!", "OK");
			potvrdiLozinku.PlaceholderColor = Colors.Red;
			potvrdiLabel.TextColor = Colors.Red;
			return;
		}
		var Email = await App._dbService.GetKorisnikAsync(korisnik.Email);
		if (Email != null)
		{
			await DisplayAlert("Greska!", "Unešena e-mail adresa je već unesena od strane drugog korisnika! Molimo  unesite novu e-mail adresu.", "OK");
			email.PlaceholderColor = Colors.Red;
			emailLabel.TextColor = Colors.Red;
			return;
		}
		if (lozinka.Text != potvrdiLozinku.Text)
		{
			await DisplayAlert("Greska!", "Lozinke se ne poklapaju!", "OK");
			potvrdiLozinku.PlaceholderColor = Colors.Red;
			potvrdiLabel.TextColor = Colors.Red;
			return;
		}
		
		await App._dbService.SaveKorisnikAsync(korisnik);
		await App._dbService.AppendUserToJsonAsync(korisnik);

		await Navigation.PushAsync(new LoginPage());
		
	}
}