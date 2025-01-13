using SQLite;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviz
{
	[Table("korisnik")]
	public class Korisnik
	{
		[PrimaryKey]
		[AutoIncrement]
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("surname")]
		public string Surname { get; set; }

		[Column("email")]
		public string Email { get; set; }
		[Column("password")]
		public string Password { get; set; }

	}
}
