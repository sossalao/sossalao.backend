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
    [Authorize("Bearer", Roles = "Master, HotScissor")]

    public class LoginController : Controller
	{
        readonly DataBaseContext context;
        public LoginController(DataBaseContext contexto)
        {
            this.context = contexto;
        }
        [Authorize("Bearer", Roles = "Master")]
        [HttpPost]
        public IActionResult CreateLogin([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardCreate);

            Security security = new Security();
            login.password = security.cryptopass(login.password);

            context.TB_Login.Add(login);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureCreate);
            else
                return Ok(login);
        }

        [HttpGet]
        public IEnumerable<Login> ReadLogin()
        {
            return context.TB_Login.ToList();
        }

        [HttpGet("{id}")]
        public Login ReadOneLogin(int id)
        {
            return context.TB_Login.Where(x => x.IdLogin == id).FirstOrDefault();
        }
        [HttpGet("p/{id}")]
        public Login ReadOneLoginPeople(int id)
        {
            return context.TB_Login.Where(x => x.peopleId == id).FirstOrDefault();
        }
    	[Authorize("Bearer", Roles = "Master")]
        [HttpPut("{id}")]
        public IActionResult UpdateLogin([FromBody] Login login, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Login.Where(y => y.IdLogin == id).FirstOrDefault();

            x.accessLevel = login.accessLevel;
            x.password = login.password;
            x.typeArea = login.typeArea;
            x.typeEmployee = login.typeEmployee;

            context.TB_Login.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureUpdate);
            else
                return Ok(login);
        }

        [HttpPut("password/{id}")]
        public IActionResult UpdatePasswordLogin([FromBody] Login login, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultMessages.nonStandardUpdate);
            var x = context.TB_Login.Where(y => y.IdLogin == id).FirstOrDefault();

            x.password = login.password;

            context.TB_Login.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest(DefaultMessages.internalfailureUpdate);
            else
                return Ok(login);
        }

        [HttpDelete("i/{id}")]
        public IActionResult DeleteActiveLogin(int id)
        {
            var x = context.TB_Login.Where(y => y.IdLogin == id).FirstOrDefault();
            if (x == null)
                return BadRequest(DefaultMessages.notFound);
            x.isActive = 0;

            context.TB_Login.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest();
            else
                return Ok(ReadOneLogin(id));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLogin(int id)
        {
            var x = context.TB_Login.Where(y => y.IdLogin == id).FirstOrDefault();
            if (x == null)
                return BadRequest(DefaultMessages.notFound);

            context.TB_Login.Remove(x);
            int rs = context.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}
