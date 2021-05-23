using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace sossalao.Core.Models.DTO
{
    public partial class EntidadeDTO
    {
        [JsonProperty("procedure_id")]
        public int ProcedureId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name_procedure")]
        public string NameProcedure { get; set; }

        [JsonProperty("estimitedTime")]
        public TimeSpan EstimitedTime { get; set; }

        [JsonProperty("type_area")]
        public TypeArea TypeArea { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("stockprocedure")]
        public List<StockprocedureDTO> Stockprocedure { get; set; }
    }

    public partial class StockprocedureDTO
    {
        [JsonProperty("required_quantity")]
        public int RequiredQuantity { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("type_product")]
        public TypeProduct TypeProduct { get; set; }

        [JsonProperty("product")]
        public ProductDTO Product { get; set; }

		public static implicit operator List<object>(StockprocedureDTO v)
		{
			throw new NotImplementedException();
		}
	}

    public partial class ProductDTO
    {
        [JsonProperty("idProduct")]
        public int IdProduct { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class Entidade
    {
        public static Entidade FromJson(string json) => JsonConvert.DeserializeObject<Entidade>(json, DTO.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Entidade self) => JsonConvert.SerializeObject(self, DTO.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }


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
