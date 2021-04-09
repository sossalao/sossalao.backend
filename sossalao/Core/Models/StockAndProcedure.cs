using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Models
{
	public class StockAndProcedure
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idStockAndProcedure { get; set; }
		[ForeignKey("Stock"), Required]
		public int stockId { get; set; }
		[ForeignKey("Procedure"), Required]
		public int procedureId { get; set; }
		[Required]
		public int requiredQuantity  { get; set; }

		public Stock Stock { get; set; }
		public Procedure Procedure { get; set; }
	}
}
