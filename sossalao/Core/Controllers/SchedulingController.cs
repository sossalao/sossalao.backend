using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sossalao.Core.Data;
using sossalao.Core.Models;
using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return BadRequest(DefaultMessages.nonStandardCreate);
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

        [HttpPut("{id}")]
        public IActionResult UpdateScheduling([FromBody] Scheduling scheduling, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Scheduling.Where(y => y.idScheduling == id).FirstOrDefault();

            x.checkIn = scheduling.checkIn;
            x.checkOut = scheduling.checkOut;
            x.clientId = scheduling.clientId;
            x.employeeId = scheduling.employeeId;
            x.saleId = scheduling.saleId;
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
