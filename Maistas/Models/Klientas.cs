using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
    public class Klientas
	{
		[DisplayName("Asmens kodas")]
		[Required]
		public string AsmensKodas { get; set; }

		[DisplayName("Vardas")]
		[Required]
		public string Vardas { get; set; }

		[DisplayName("Pavardė")]
		[Required]
		public string Pavarde { get; set; }

		[DisplayName("Gimimo data")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Required]
		public DateTime? GimimoData { get; set; }

		[DisplayName("Telefonas")]
		[Required]
		public string Telefonas { get; set; }

		[DisplayName("Elektroninis paštas")]
		[EmailAddress]
		[Required]
		public string Epastas { get; set; }

		[DisplayName("Abonimento numeris")]
		[Required]
		public int FkAbonimentas { get; set; }
	}
}