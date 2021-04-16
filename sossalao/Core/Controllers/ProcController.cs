using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sossalao.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Controllers
{
	[Route("api/[controller]")]
    [Authorize("Bearer", Roles = "Master, HotScissor, BluntScissor")]

    public class ProcController : Controller
	{

        readonly DataBaseContext _context;
        public ProcController(DataBaseContext contexto)
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

                var query = (from p in _context.TB_Procedure
                             join sap in _context.TB_StockAndProcedure on p.idProcedure equals sap.procedureId
                             join s in _context.TB_Stock on sap.stockId equals s.idStock
                             join pr in _context.TB_Product on s.productId equals pr.idProduct
                             select new
                             {
                                 procedure_id = p.idProcedure,
                                 name_procedure = p.name,
                                 estimited_time = p.estimitedTime,
                                 type_area = p.typeArea,
                                 price = p.price,
                                 required_quantity = sap.requiredQuantity,
                                 quantity = s.quantity,
                                 type_product = s.typeProduct,
                                 product = pr
                             }).ToList();
                return Ok(query);
			}
		}
	}
}
