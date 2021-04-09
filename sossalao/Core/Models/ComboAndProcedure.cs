using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Models
{
	public class ComboAndProcedure
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idComboProcedure { get; set; }
		[ForeignKey("Combo"), Required]
		public int comboId { get; set; }
		[ForeignKey("Procedure"), Required]
		public int procedureId { get; set; }

		public Combo Combo { get; set; }
		public Procedure Procedure { get; set; }
	}
}
