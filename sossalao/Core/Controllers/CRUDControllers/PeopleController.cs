using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sossalao.Core.Data;
using sossalao.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sossalao.Core.Utils;

namespace sossalao.Core.Controllers
{
	[Route("api/[controller]")]
	//[Authorize("Bearer", Roles = "Master, HotScissor, BluntScissor")]
	public class PeopleController : Controller
	{
		readonly DataBaseContext context;
		public PeopleController(DataBaseContext contexto)
		{
			this.context = contexto;
		}
		[HttpPost]
		public IActionResult CreatePeople([FromBody] People people)
		{
			if (!ModelState.IsValid)
				return BadRequest(DefaultMessages.nonStandardCreate);
			context.TB_People.Add(people);
			int rs = context.SaveChanges();
			if (rs < 1)
				return BadRequest(DefaultMessages.internalfailureCreate);
			else
				return Ok(people);
		}
		[HttpPost("list")]
		public IActionResult CreatePeopleList([FromBody] List<People> peoples)
		{
			int rs = 0;
			int x = peoples.Count();
			foreach (People people in peoples)
			{
				if (!ModelState.IsValid)
					return ValidationProblem(DefaultMessages.nonStandardCreate,null,422,"Por favor, valide a lista que esta enviando!");
				context.TB_People.Add(people);
				rs = rs + context.SaveChanges();
			}
			if (rs < x)
				return BadRequest(DefaultMessages.internalfailureCreate);
			else
				return Ok(peoples);
		}

		[HttpGet]
		public IEnumerable<People> ReadPeople(string? name)
		{
			if (name != null)
			{
				return context.TB_People.Where(x => x.name.Contains(name)).ToList();
			}
			else
			{
				return context.TB_People.ToList();
			}
		}

		[HttpGet("{id}")]
		public People ReadOnePeople(int id)
		{
			return context.TB_People.Where(x => x.idPeople == id).FirstOrDefault();
		}

		[Authorize("Bearer", Roles = "Master, HotScissor")]
		[HttpPut("{id}")]
		public IActionResult UpdatePeople([FromBody] People people, int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(DefaultMessages.nonStandardUpdate);
			var x = context.TB_People.Where(y => y.idPeople == id).FirstOrDefault();

			x.name = people.name;
			x.phoneNumber = people.phoneNumber;
			x.email = people.email;
			x.typePeople = people.typePeople;

			context.TB_People.Update(x);
			int rs = context.SaveChanges();
			if (rs < 1)
				return BadRequest(DefaultMessages.internalfailureUpdate);
			else
				return Ok(people);
		}

		[HttpDelete("{id}")]
		[Authorize("Bearer", Roles = "Master, HotScissor")]
		public IActionResult DeletePeople(int id)
		{
			var x = context.TB_People.Where(y => y.idPeople == id).FirstOrDefault();
			if (x == null)
				return BadRequest(DefaultMessages.notFound);

			context.TB_People.Remove(x);
			int rs = context.SaveChanges();
			if (rs > 0)
				return Ok();
			else
				return BadRequest();
		}
	}
}
