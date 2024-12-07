using SQLite;

namespace Kviz
{
	[Table("pitanja")]
	public class Pitanje
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("id")]
		public int Id { get; set; }

		[Column("tekst")]
		public string Tekst { get; set; }

		[Column("tacan_odgovor_id")]

		public int TacanOdgovorId { get; set; }
	}
}
