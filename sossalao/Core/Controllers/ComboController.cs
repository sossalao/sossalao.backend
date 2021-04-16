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

    public class ComboController : Controller
	{
        readonly DataBaseContext context;
        public ComboController(DataBaseContext contexto)
        {
            this.context = contexto;
        }
		#region Controller Combo
		[HttpPost]
        public IActionResult CreateCombo([FromBody] Combo combo)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardCreate);
            context.TB_Combo.Add(combo);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureCreate);
            else
                return Ok(combo);
        }

        [HttpGet]
        public IEnumerable<Combo> ReadCombo()
        {
            return context.TB_Combo.ToList();
        }

        [HttpGet("{id}")]
        public Combo ReadOneCombo(int id)
        {
            return context.TB_Combo.Where(x => x.idCombo == id).FirstOrDefault();
        }

        [Authorize("Bearer", Roles = "Master, HotScissor")]
        [HttpPut("{id}")]
        public IActionResult UpdateCombo([FromBody] Combo combo, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Combo.Where(y => y.idCombo == id).FirstOrDefault();

            x.name = combo.name;
            x.description = combo.description;
            x.price = combo.price;

            context.TB_Combo.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureUpdate);
            else
                return Ok(combo);
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer", Roles = "Master, HotScissor")]
        public IActionResult DeleteCombo(int id)
        {
            var x = context.TB_Combo.Where(y => y.idCombo == id).FirstOrDefault();
            if (x == null)
                return BadRequest(DefaultMessages.notFound);

            context.TB_Combo.Remove(x);
            int rs = context.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
        #endregion
        #region Controller ComboAndProcedure
        [HttpPost("sap")]
        public IActionResult CreateSAP([FromBody] ComboAndProcedure cap)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardCreate);
            context.TB_ComboAndProcedure.Add(cap);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureCreate);
            else
                return Ok(cap);
        }
		#endregion
	}
}
