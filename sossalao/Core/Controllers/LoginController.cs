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
                return BadRequest("Não foi possivel cadastrar, dados fora de padrão!");

            Security security = new Security();
            login.password = security.cryptopass(login.password);

            context.TB_Login.Add(login);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest("Houve uma falha interna e não foi possivel cadastrar");
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

        [HttpPut("{id}")]
        public IActionResult UpdateLogin([FromBody] Login login, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("não foi possivel enviar os dados para atualizar");
            var x = context.TB_Login.Where(y => y.IdLogin == id).FirstOrDefault();

            x.accessLevel = login.accessLevel;
            x.password = login.password;
            x.typeArea = login.typeArea;
            x.typeEmployee = login.typeEmployee;

            context.TB_Login.Update(x);
            int rs = context.SaveChanges();
            if (rs < 1)
                return BadRequest("Houve uma falha interna e não foi possivel atualizar");
            else
                return Ok(login);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLogin(int id)
        {
            var x = context.TB_Login.Where(y => y.IdLogin == id).FirstOrDefault();
            if (x == null)
                return BadRequest("Usuario não encontrado.");

            context.TB_Login.Remove(x);
            int rs = context.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}
