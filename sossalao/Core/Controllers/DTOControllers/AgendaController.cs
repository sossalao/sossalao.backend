using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sossalao.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Controllers.DTOControllers
{
	[Route("api/[controller]")]
	[Authorize("Bearer", Roles = "Master, HotScissor, BluntScissor")]

	public class AgendaController : Controller
	{

		readonly DataBaseContext _context;
		public AgendaController(DataBaseContext contexto)
		{
			this._context = contexto;
		}

		[HttpGet]
		public IActionResult Read()
		{
			if (!ModelState.IsValid)
				return BadRequest("deuruim");
			else
			{
				var query = (from a in _context.TB_Scheduling
							 join b in _context.TB_Login on a.employeeId equals b.IdLogin
							 join c in _context.TB_People on a.clientId equals c.idPeople
							 join d in _context.TB_People on b.peopleId equals d.idPeople
							 join e in _context.TB_Procedure on a.procedureId equals e.idProcedure

							 select new
							 {
								id_scheduling = a.idScheduling,
								status_scheduling =  Convert.ToString(a.status),//a.status,
								check_in = a.checkIn,
								check_out = a.checkOut,
								//employee_id = a.employeeId,
								employee = d.name,
								procedure = e.name,
								 //client_id = a.clientId
								 client = c.name
							 }).ToList();

				return Ok(query);
			}
		}

		
		}
	}

