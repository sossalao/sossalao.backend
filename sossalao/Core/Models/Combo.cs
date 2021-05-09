using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Models
{
	public class Combo
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idCombo { get; set; }
		[Required, StringLength(150, ErrorMessage = "Você precisa inserir o nome do combo.")]
		public string name { get; set; }
		[Required, StringLength(255, ErrorMessage = "Você precisa inserir a descrição do combo.")]
		public string description { get; set; }
		[Required(ErrorMessage = "Você precisa inserir o preço do combo")]
		public decimal price { get; set; }

		//public ICollection<ComboAndProcedure> ComboAndProcedure { get; set; }

	}
}
