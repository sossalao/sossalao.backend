using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sossalao.Core.Models
{
	public class Procedure
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idProcedure { get; set; }
		[Required, StringLength(150, ErrorMessage = "Você precisa inserir o nome do procedimento.")]
		public string name { get; set; }
		[Required, StringLength(255, ErrorMessage = "Você precisa inserir a descrição do procedimento.")]
		public string description { get; set; }
		[Required(ErrorMessage = "Você precisa inserir o preço do procedimento")]
		public decimal price { get; set; }
		public TimeSpan estimitedTime { get; set; }
		public TypeArea typeArea { get; set; }

		public ICollection<ComboAndProcedure> ComboAndProcedure { get; set; }
		public ICollection<StockAndProcedure> ProcedureAndStocks { get; set; }
		public Sale Sale { get; set; }
	}
}
