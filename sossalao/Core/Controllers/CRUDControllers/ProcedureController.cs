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
		public IEnumerable<Procedure> GetProcdureAreas(int id)
		{
			return context.TB_Procedure.Where(x => Convert.ToInt32(x.typeArea) == id).ToList();
		}

		[Authorize("Bearer", Roles = "Master")]
		[HttpPut("antigo/{id}")]
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
		[Authorize("Bearer", Roles = "Master")]
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

	}
}
