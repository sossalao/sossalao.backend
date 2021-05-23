using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sossalao.Core.Data;
using sossalao.Core.Models;
using sossalao.Core.Models.DTO;
using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Controllers
{
	[Authorize("Bearer", Roles = "Master, HotScissor, BluntScissor")]
	[Route("api/[controller]")]
	public class ProcedureController : Controller
	{
		readonly DataBaseContext context;
		public ProcedureController(DataBaseContext contexto)
		{
			this.context = contexto;
		}
		#region Controller Procedure
		
		[HttpPost("antigo")]
		public IActionResult CreateProcedure([FromBody] Procedure procedure)
		{
			if (!ModelState.IsValid)
				return BadRequest(DefaultMessages.nonStandardCreate);
			context.TB_Procedure.Add(procedure);
			int rs = context.SaveChanges();
			if (rs < 1)
				return BadRequest(DefaultMessages.internalfailureCreate);
			else
				return Ok(procedure);
		}

		[HttpGet]
		public IEnumerable<Procedure> ReadProcedure()
		{
			return context.TB_Procedure.ToList();
		}


		[HttpGet("{id}")]
		public Procedure ReadOneProcedure(int id)
		{
			return context.TB_Procedure.Where(x => x.idProcedure == id).FirstOrDefault();
		}
		[HttpGet("get-area/{id}")]
		public Procedure GetProcdureAreas(int id)
		{
			return context.TB_Procedure.Where(x => Convert.ToInt32(x.typeArea) == id).FirstOrDefault();
		}

		[Authorize("Bearer", Roles = "Master, HotScissor")]
		[HttpPut("{id}")]
		public IActionResult UpdateProcedure([FromBody] Procedure procedure, int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(DefaultMessages.nonStandardUpdate);
			var x = context.TB_Procedure.Where(y => y.idProcedure == id).FirstOrDefault();

			x.name = procedure.name;
			x.description = procedure.description;
			x.estimitedTime = procedure.estimitedTime;
			x.price = procedure.price;

			context.TB_Procedure.Update(x);
			int rs = context.SaveChanges();
			if (rs < 1)
				return BadRequest(DefaultMessages.internalfailureUpdate);
			else
				return Ok(procedure);
		}

		[HttpDelete("{id}")]
		[Authorize("Bearer", Roles = "Master, HotScissor")]
		public IActionResult DeleteProcedure(int id)
		{
			var x = context.TB_Procedure.Where(y => y.idProcedure == id).FirstOrDefault();
			if (x == null)
				return BadRequest(DefaultMessages.notFound);

			context.TB_Procedure.Remove(x);
			int rs = context.SaveChanges();
			if (rs > 0)
				return Ok();
			else
				return BadRequest();
		}
		#endregion

		#region Controller StockAndProcedure
		[HttpPost("sap")]
		public IActionResult CreateSAP([FromBody] StockAndProcedure sap)
		{
			if (!ModelState.IsValid)
				return BadRequest(DefaultMessages.nonStandardCreate);
			context.TB_StockAndProcedure.Add(sap);
			int rs = context.SaveChanges();
			if (rs < 1)
				return BadRequest(DefaultMessages.internalfailureCreate);
			else
				return Ok(sap);
		}
		[HttpGet("sap/{id}")]
		public StockAndProcedure ReadSAP(int id)
		{
			return context.TB_StockAndProcedure.Where(x => x.idStockAndProcedure == id).FirstOrDefault();
		}
		[HttpGet("sap-list/{id}")]
		public StockAndProcedure ReadSAPlist(int id)
		{
			return context.TB_StockAndProcedure.Where(x => x.procedureId == id).FirstOrDefault();
		}
		#endregion
	}
}
