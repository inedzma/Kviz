using SQLite;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviz
{
	[Table("rezultat")]
	public class Rezultat
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("id")]
		public int Id { get; set; }

		[Column("email")]
		public string EmailKorisnika { get; set; }

		[Column("kontinent")]
		public string Kontinent { get; set; }

		[Column("zastave")]
		public bool isZastave { get; set; }

		[Column("odgovori")]
		public int TacniOdgovori { get; set; }

		[Column("pitanja")]
		public int BrojPitanja { get; set; }

		[Column("vrijeme")]
		public DateTime DatumVrijeme { get; set; }
	}
}
