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
		[ForeignKey("Procedure"), Required]
		public int procedureId { get; set; }
		[Required]
		public StatusScheduling status { get; set; }

		private Procedure Procedure { get; set; }
		private Login Login { get; set; }
		private People People { get; set; }

	}
}
