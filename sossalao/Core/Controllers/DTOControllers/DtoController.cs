using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using sossalao.Core.Data;
using sossalao.Core.Models;
using sossalao.Core.Models.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace EiCara
{
	[ApiController]
	[Route("api")]
	[Authorize("Bearer", Roles = "Master, HotScissor, BluntScissor")]


	public class DtoController : ControllerBase
	{
		readonly DataBaseContext _context;
		public DtoController(DataBaseContext context)
		{
			this._context = context;
		}
		
		[HttpGet("{id}")]
		public async Task<ActionResult<Procedure>> GetItemProcedure(int id)
		{
			var recipe = await _context.TB_Procedure.FindAsync(id);

			if (recipe == null)
			{
				return NotFound();
			}

			return ProcedureToDTO(recipe);
		}
		[Authorize("Bearer", Roles = "Master")]
		[HttpPost("procedure")]
		public async Task<ActionResult<ProcedureDTO>> CreateProcedureDTO(ProcedureDTO procedureDTO)
		{
			var rs = new Procedure
			{
				name = procedureDTO.name,
				description = procedureDTO.description,
				price = procedureDTO.price,
				estimitedTime = new System.TimeSpan(0, procedureDTO.estimitedTime, 0),
				typeArea = procedureDTO.typeArea
			};

			_context.TB_Procedure.Add(rs);
			await _context.SaveChangesAsync();

			return CreatedAtAction(
				nameof(GetItemProcedure),
				new { id = rs.idProcedure },
				ProcedureToDTO(rs));
		}
		[Authorize("Bearer", Roles = "Master")]
		[HttpPut("procedure/{id}")]
		public async Task<ActionResult<ProcedureDTO>> UpdateProcedureDTO(ProcedureDTO procedureDTO, int id)
		{
			var x = _context.TB_Procedure.Where(y => y.idProcedure == id).FirstOrDefault();

			x.name = procedureDTO.name;
			x.description = procedureDTO.description;
			x.estimitedTime = new System.TimeSpan(0, procedureDTO.estimitedTime, 0);
			x.typeArea = procedureDTO.typeArea;
			
			_context.TB_Procedure.Update(x);
			await _context.SaveChangesAsync();

			return CreatedAtAction(
				nameof(GetItemProcedure),
				new { id = x.idProcedure },
				ProcedureToDTO(x));
		}
		private static Procedure ProcedureToDTO(Procedure procedure) =>
			new Procedure
			{
				idProcedure = procedure.idProcedure,
				name = procedure.name,
				description = procedure.description,
				price = procedure.price,
				estimitedTime = procedure.estimitedTime,
				typeArea = procedure.typeArea
			};
	}
}
