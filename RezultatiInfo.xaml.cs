namespace Kviz;

public partial class RezultatiInfo : TabbedPage
{
	public RezultatiInfo(string initialTab = "Rezultati")
	{
		InitializeComponent();

		// Prebaci na odgovarajući tab
		if (initialTab == "Info")
		{
			CurrentPage = Children[1]; // Info tab
		}
		else
		{
			CurrentPage = Children[0]; // Rezultati tab
		}
	}
}
