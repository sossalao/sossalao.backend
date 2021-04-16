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
    public class SaleController : Controller
	{
        readonly DataBaseContext context;
        public SaleController(DataBaseContext contexto)
        {
            this.context = contexto;
        }
		#region Controller Sale
		[HttpPost]
        public IActionResult CreateSale([FromBody] Sale sale)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardCreate);
            context.TB_Sale.Add(sale);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureCreate);
            else
                return Ok(sale);
        }

        [HttpGet]
        public IEnumerable<Sale> ReadSale()
        {
            return context.TB_Sale.ToList();
        }

        [HttpGet("{id}")]
        public Sale ReadOneSale(int id)
        {
            return context.TB_Sale.Where(x => x.idSale == id).FirstOrDefault();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSale([FromBody] Sale sale, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Sale.Where(y => y.idSale == id).FirstOrDefault();

            x.clientId = sale.clientId;
            x.procedureId = sale.procedureId;
            x.comboId = sale.comboId;
            x.amount = sale.amount;
            x.discount = sale.discount;

            context.TB_Sale.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureUpdate);
            else
                return Ok(sale);
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer", Roles = "Master, HotScissor")]
        public IActionResult DeleteSale(int id)
        {
            var x = context.TB_Sale.Where(y => y.idSale == id).FirstOrDefault();
            if (x == null)
                return BadRequest(DefaultMessages.notFound);

            context.TB_Sale.Remove(x);
            int rs = context.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
        #endregion
	}
}
