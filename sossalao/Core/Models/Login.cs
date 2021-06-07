using sossalao.Core.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sossalao.Core.Models
{
	public class Login
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdLogin { get; set; }
		[Required]
		[StringLength(30, MinimumLength = 4, ErrorMessage = "Você precisa inserir o nome de usuario")]
		public string user { get; set; }

		[Required,StringLength(255)]
		public string password { get; set; }
		[Required]
		public TypeArea typeArea { get; set; }
		[Required]
		public TypeEmployee typeEmployee { get; set; }
		[Required]
		public AccessLevel accessLevel { get; set; }
		[ForeignKey("People"), Required]
		public int peopleId { get; set; }

		private People People { get; set; }
	}
}
