using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sossalao.Core.Models
{
	public class Supplier
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idSupplier { get; set; }
		[Required, StringLength(150, ErrorMessage = "Você precisa inserir o nome do fornecedor.")]
		public string supplierName { get; set; }
		[Required, Phone, MinLength(10), StringLength(15, ErrorMessage = "Você precisa inserir o numero de telefone do fornecedor.")]
		public string phoneNumber { get; set; }
		[Required, StringLength(255, ErrorMessage = "Você precisa inserir a descrição do fornecedor.")]
		public string description { get; set; }
	}
}
