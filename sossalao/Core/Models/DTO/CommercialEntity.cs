using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace sossalao.Core.Models.DTO
{
    #region ProcedureDTO
    public class ProcedureDTO
	{
		public int idProcedure { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public decimal price { get; set; }
		public int estimitedTime { get; set; }
		public TypeArea typeArea { get; set; }
	}
	#endregion
}
