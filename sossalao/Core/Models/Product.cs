using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sossalao.Core.Models
{
	public class Product
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idProduct { get; set; }
		[Required, StringLength(150, ErrorMessage = "Você precisa inserir o nome do produto.")]
		public string name { get; set; }
		[Required, StringLength(150, ErrorMessage = "Você precisa inserir a marca do produto.")]
		public string make { get; set; }
		[Required, StringLength(255, ErrorMessage = "Você precisa inserir a descrição do produto.")]
		public string description { get; set; }

	}
}
