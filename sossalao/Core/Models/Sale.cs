using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sossalao.Core.Models
{
	public class Sale
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idSale { get; set; }
		[Required(ErrorMessage = "Você precisa inserir o valor total da Venda")]
		public decimal amount { get; set; }
		[ForeignKey("People"), Required]
		public int clientId { get; set; }
		[ForeignKey("Procedure"), Required]
		public int procedureId { get; set; }
		[ForeignKey("Combo"), Required]
		public int comboId { get; set; }
		public decimal discount { get; set; }

		public Scheduling Scheduling { get; set; }
		public People People { get; set; }
		public Procedure procedure { get; set; }
		public Combo Combo { get; set; }


	}
}
