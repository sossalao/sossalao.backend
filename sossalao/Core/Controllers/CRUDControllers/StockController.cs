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
	[Authorize("Bearer", Roles = "Master, HotScissor, BluntScissor")]
	public class StockController : Controller
	{
        readonly DataBaseContext context;
        public StockController(DataBaseContext contexto)
        {
            this.context = contexto;
        }

		#region Controller Stock
		[HttpPost("api/stock")]
        public IActionResult CreateStock([FromBody] Stock stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardCreate);
            context.TB_Stock.Add(stock);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureCreate);
            else
                return Ok(stock);
        }

        [HttpGet("api/stock")]
        public IEnumerable<Stock> ReadStock()
        {
            return context.TB_Stock.ToList();
        }

        [HttpGet("api/stock/{id}")]
        public Stock ReadOneStock(int id)
        {
            return context.TB_Stock.Where(x => x.idStock == id).FirstOrDefault();
        }

        [HttpPut("api/stock/{id}")]
        public IActionResult UpdateStock([FromBody] Stock stock, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Stock.Where(y => y.idStock == id).FirstOrDefault();

            x.quantity = stock.quantity;
            x.typeProduct = stock.typeProduct;
            x.productId = stock.productId;
            x.supplierId = stock.supplierId;

            context.TB_Stock.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureUpdate);
            else
                return Ok(stock);
        }

        [HttpDelete("api/stock/{id}")]
        public IActionResult DeleteStock(int id)
        {
            var x = context.TB_Stock.Where(y => y.idStock == id).FirstOrDefault();
            if (x == null)
                return BadRequest(DefaultMessages.notFound);

            context.TB_Stock.Remove(x);
            int rs = context.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
		#endregion

		#region Controller Product
		[HttpPost("api/stock/product")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardCreate);
            context.TB_Product.Add(product);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureCreate);
            else
                return Ok(product);
        }
        [HttpGet("api/stock/product")]
        public IEnumerable<Product> ReadProduct()
        {
            return context.TB_Product.ToList();
        }

        [HttpGet("api/stock/product/{id}")]
        public Product ReadOneProduct(int id)
        {
            return context.TB_Product.Where(x => x.idProduct == id).FirstOrDefault();
        }

        [HttpPut("api/stock/product/{id}")]
        public IActionResult UpdateProduct([FromBody] Product product, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Product.Where(y => y.idProduct == id).FirstOrDefault();

            x.make = product.make;
            x.description = product.description;
            x.name = product.name;
     
            context.TB_Product.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureUpdate);
            else
                return Ok(product);
        }

        [HttpDelete("/product/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var x = context.TB_Product.Where(y => y.idProduct == id).FirstOrDefault();
            if (x == null)
                return BadRequest(DefaultMessages.notFound);

            context.TB_Product.Remove(x);
            int rs = context.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
		#endregion

		#region Controller Supplier
		[HttpPost("api/stock/supplier")]
        public IActionResult CreateSupplier([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardCreate);
            context.TB_Supplier.Add(supplier);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureCreate);
            else
                return Ok(supplier);
        }
        [HttpGet("api/stock/supplier/{id}")]
        public Supplier ReadOneSupplier(int id)
        {
            return context.TB_Supplier.Where(x => x.idSupplier == id).FirstOrDefault();
        }

        [HttpPut("api/stock/supplier/{id}")]
        public IActionResult UpdateSupplier([FromBody] Supplier supplier, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Supplier.Where(y => y.idSupplier == id).FirstOrDefault();

            x.phoneNumber = supplier.phoneNumber;
            x.description = supplier.description;
            x.supplierName = supplier.supplierName;

            context.TB_Supplier.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureUpdate);
            else
                return Ok(supplier);
        }

        [HttpDelete("api/stock/supplier/{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            var x = context.TB_Supplier.Where(y => y.idSupplier == id).FirstOrDefault();
            if (x == null)
                return BadRequest(DefaultMessages.notFound);

            context.TB_Supplier.Remove(x);
            int rs = context.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
		#endregion

	}
}
