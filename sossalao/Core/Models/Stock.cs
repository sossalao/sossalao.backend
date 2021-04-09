using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Models
{
	public class Stock
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idStock { get; set; }
		[ForeignKey("Product"), Required]
		public int productId { get; set; }
		[ForeignKey("Supplier"), Required]
		public int supplierId { get; set; }
		[Required]
		public int quantity { get; set; }
		public TypeProduct typeProduct { get; set; }

		public ICollection<StockAndProcedure> stoqPro { get; set; }
		public Product Product { get; set; }
		public Supplier Supplier { get; set; }
	}
}
