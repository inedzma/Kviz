using SQLite;
namespace Kviz
{
	[Table("drzava")]
	public class Drzava
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("id")]
		public int Id { get; set; }

		[Column("naziv")]
		public string Naziv { get; set; }

		[Column("zastava")]
		public string Zastava { get; set; }

		[Column("glavni_grad")]
		public string GlavniGrad { get; set; }

		[Column("kontinent")]

		public string Kontinent { get; set; }
	}
}
