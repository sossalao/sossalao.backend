using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sossalao.Core.Data;
using sossalao.Core.Models;
using sossalao.Core.Models.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace EiCara
{
	[ApiController]
	[Route("api")]

	public class DtoController : ControllerBase
	{
		readonly DataBaseContext _context;
		public DtoController(DataBaseContext context)
		{
			this._context = context;
		}
		[HttpGet("v3")]
		public IQueryable<StockprocedureDTO> GetBooks()
		{
			var ProdDTO = from p in _context.TB_Product
						  join sst in _context.TB_Stock on p.idProduct equals sst.productId
						  select new ProductDTO
						  {
							  IdProduct = p.idProduct,
							  Name = p.name,
							  Make = p.make,
							  Description = p.description
						  };
			var SaPDTO = from sap in _context.TB_StockAndProcedure join s in _context.TB_Stock on sap.stockId equals s.idStock
						 select new StockprocedureDTO
						 { RequiredQuantity = sap.requiredQuantity, Quantity = s.quantity, TypeProduct = s.typeProduct, Product = (ProductDTO)ProdDTO };
			//var books = from b in _context.TB_Procedure
			//			select new EntidadeDTO()
			//			{ ProcedureId = b.idProcedure, NameProcedure = b.name, 
			//				Description = b.description, EstimitedTime = b.estimitedTime, 
			//				TypeArea = b.typeArea, Price = b.price, Stockprocedure = (System.Collections.Generic.List<StockprocedureDTO>)SaPDTO };
			return SaPDTO;
		}
		//public async Task<IHttpActionResult> GetBook(int id)
		//{
		//	Stockprocedure carObj = new Stockprocedure();
		//	var book = await _context.TB_Procedure.Include(b => b.idProcedure).Select(b =>
		//		new Entidade()
		//		{
		//			ProcedureId = b.idProcedure,
		//			NameProcedure = b.name,
		//			Description = b.description,
		//			Price = b.price,
		//			TypeArea = b.typeArea,
		//			Stockprocedure = carObj
		//}).SingleOrDefaultAsync(b => b.ProcedureId == id);
		//	var vs = await _context.TB_StockAndProcedure.Include(b => b.procedureId).Select(b =>
		//			new Stockprocedure()
		//			{

		//			}
		//			).SingleOrDefaultAsync(b => b)
		//	if (book == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(book);
		//}
		//[HttpGet("v3")]
		//public IQueryable<Entidade> GetBooks()
		//{
		//	Stockprocedure carObj = new Stockprocedure();
		//	var books = from b in _context.TB_Procedure
		//				select new Entidade()
		//				{ ProcedureId = b.idProcedure, NameProcedure = b.name, Description = b.description, Price = b.price, TypeArea = b.typeArea, Stockprocedure = b.ProcedureAndStocks };
		//	return books;
		//}
		//[HttpGet("entidade/{id}")]
		//public async Task<ActionResult<Entidade>> GetTodoItem(int id)
		//{
		//	var recipe = await _context.TB_Procedure.FindAsync(id);
		//	var recipe1 = await _context.TB_StockAndProcedure.FindAsync(id);
		//	var recipe2 = await _context.TB_Stock.FindAsync(id);
		//	var recipe3 = await _context.TB_Product.FindAsync(id);

		//	if (recipe == null)
		//	{
		//		return NotFound();
		//	}

		//	return ItemToDTO2(recipe);
		//}
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
