

namespace Kviz;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

		Task.Run(async () => await App._dbService.LoadUsersFromJsonAsync());
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(email.Text))
		{
			await DisplayAlert("Greška", "Molimo unesite e-mail adresu!", "OK");
			email.PlaceholderColor = Colors.Red;
			emailLabel.TextColor = Colors.Red;
			return;
		}
		if (string.IsNullOrEmpty(lozinka.Text)) {
			await DisplayAlert("Greška", "Molimo unesite lozinku!", "OK");
			lozinka.PlaceholderColor = Colors.Red;
			lozinkaLabel.TextColor = Colors.Red;
			return;
		}
		var korisnik = await App._dbService.GetKorisnikAsync(email.Text);
		if(korisnik == null)
		{
			await DisplayAlert("Greška", "Korisnik sa unesenom adrsom ne postoji!\n Molimo unesite novu e-mail adresu ili se registrujte.", "OK");
			email.PlaceholderColor = Colors.Red;
			emailLabel.TextColor = Colors.Red;
			return;
		}
		else if(korisnik.Password != lozinka.Text)
		{
			await DisplayAlert("Greška", "Unesena lozinka je pogrešna!\nPokušajte ponovo.", "OK");
			lozinka.PlaceholderColor = Colors.Red;	
			lozinkaLabel.TextColor = Colors.Red;
			return;
		}
		App.Email = email.Text;

		await Navigation.PushAsync(new MainPage());

	}

	private async void Button_Clicked_1(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new RegistrationPage());

	}
}