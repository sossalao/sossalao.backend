using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sossalao.Core.Data;
using sossalao.Core.Models;
using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace sossalao.Core.Controllers
{
    [Route("api/[controller]")]
	[Authorize("Bearer", Roles = "Master, HotScissor, BluntScissor")]
	public class SchedulingController : Controller
	{
        readonly DataBaseContext context;
        public SchedulingController(DataBaseContext contexto)
        {
            this.context = contexto;
        }
		#region Controller Scheduling
		[HttpPost]
        public IActionResult CreateScheduling([FromBody] Scheduling scheduling)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(DefaultMessages.nonStandardCreate, null, 422, "por favor, valide o objeto enviado.");
            context.TB_Scheduling.Add(scheduling);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureCreate);
            else
                return Ok(scheduling);
        }

        [HttpGet]
        public IEnumerable<Scheduling> ReadScheduling()
        {
            return context.TB_Scheduling.ToList();
        }

        [HttpGet("{id}")]
        public Scheduling ReadOneScheduling(int id)
        {
            return context.TB_Scheduling.Where(x => x.idScheduling == id).FirstOrDefault();
        }
        [HttpGet("range")]
        public IActionResult ReadRangeaScheduling([FromQuery] DateTime startDate, DateTime? endDate)
        {
            if(endDate < startDate){
                return ValidationProblem(DefaultMessages.DateValidationProblem, null, 422,"Valide as datas."); 
            }
            if(endDate != null && endDate >= startDate)
            {
                return Ok(context.TB_Scheduling.Where(x => x.checkIn >= startDate && x.checkOut <= endDate).ToList());
            }
            if (endDate != null && endDate == startDate)
            {
                startDate = startDate.AddHours(23);
                return Ok(context.TB_Scheduling.Where(x => x.checkIn >= endDate && x.checkOut <= startDate).ToList());
            }
            return Ok(context.TB_Scheduling.Where(x => x.checkIn >= startDate).ToList());
        }

        [HttpGet("funcionariodisponivel")]
        public IActionResult ReadRangeFuncionarioScheduling([FromQuery] DateTime startDate, DateTime? endDate, int FuncionarioDisponivel)
        {
            if (endDate < startDate && FuncionarioDisponivel == 0)
            {
                return ValidationProblem(DefaultMessages.DateValidationProblem, null, 422, "Valide as datas e funcionario.");
            }
            if (endDate != null && endDate > startDate)
            {
                return Ok(context.TB_Scheduling.Where(x => x.checkIn >= startDate && x.checkIn <= endDate && x.employeeId != FuncionarioDisponivel).ToList());
			}
			else
			{
                return BadRequest(context.TB_Scheduling.Where(x => x.checkIn >= startDate && x.checkIn <= endDate && x.employeeId == FuncionarioDisponivel).ToList());
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateScheduling([FromBody] Scheduling scheduling, int id)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Scheduling.Where(y => y.idScheduling == id).FirstOrDefault();

            x.checkIn = scheduling.checkIn;
            x.checkOut = scheduling.checkOut;
            x.clientId = scheduling.clientId;
            x.employeeId = scheduling.employeeId;
            x.procedureId = scheduling.procedureId;
            x.status = scheduling.status;

            context.TB_Scheduling.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureUpdate);
            else
                return Ok(scheduling);
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer", Roles = "Master, HotScissor")]
        public IActionResult DeleteScheduling(int id)
        {
            var x = context.TB_Scheduling.Where(y => y.idScheduling == id).FirstOrDefault();
            if (x == null)
                return BadRequest(DefaultMessages.notFound);

            context.TB_Scheduling.Remove(x);
            int rs = context.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
        #endregion
	}
}
