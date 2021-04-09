using sossalao.Core.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sossalao.Core.Models
{
	public class Scheduling
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int idScheduling { get; set; }
		[Required]
		public DateTime checkIn { get; set; }
		[Required]
		public DateTime checkOut { get; set; }
		[ForeignKey("Login"), Required]
		public int employeeId { get; set; }
		[ForeignKey("People"), Required]
		public int clientId { get; set; }
		[ForeignKey("Sale"), Required]
		public int saleId { get; set; }
		[Required]
		public StatusScheduling status { get; set; }

		public Sale Sale { get; set; }
		public Login Login { get; set; }
		public People People { get; set; }

	}
}
