using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sossalao.Core.Data;
using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Controllers.DTOControllers
{
    [Route("api/[controller]")]
    [Authorize("Bearer", Roles = "Master, HotScissor, BluntScissor")]

    public class AgendaController : Controller
    {

        readonly DataBaseContext _context;
        public AgendaController(DataBaseContext contexto)
        {
            this._context = contexto;
        }
        #region get Agenda and search by client name
        [HttpGet]
        public IActionResult ReadClient(string? nameCliente, int? statusAgenda)
        {
            if (nameCliente != null)
            {
                var query1 = (from a in _context.TB_Scheduling
                              join b in _context.TB_Login on a.employeeId equals b.IdLogin
                              join c in _context.TB_People on a.clientId equals c.idPeople
                              join d in _context.TB_People on b.peopleId equals d.idPeople
                              join e in _context.TB_Procedure on a.procedureId equals e.idProcedure

                              where c.name.Contains(nameCliente)
                              select new
                              {
                                  id_scheduling = a.idScheduling,
                                  status_scheduling = Convert.ToString(a.status),//a.status,
                                  check_in = a.checkIn,
                                  check_out = a.checkOut,
                                  //employee_id = a.employeeId,
                                  employee = d.name,
                                  procedure = e.name,
                                  procedure_price = e.price,
                                  //client_id = a.clientId
                                  client = c.name
                              }).OrderByDescending(x => x.check_in).ToList();
                return Ok(query1);
            }
            if (statusAgenda != null)
            {
                var query1 = (from a in _context.TB_Scheduling
                              join b in _context.TB_Login on a.employeeId equals b.IdLogin
                              join c in _context.TB_People on a.clientId equals c.idPeople
                              join d in _context.TB_People on b.peopleId equals d.idPeople
                              join e in _context.TB_Procedure on a.procedureId equals e.idProcedure

                              where ((int)a.status) == statusAgenda
                              select new
                              {
                                  id_scheduling = a.idScheduling,
                                  status_scheduling = Convert.ToString(a.status),//a.status,
                                  check_in = a.checkIn,
                                  check_out = a.checkOut,
                                  //employee_id = a.employeeId,
                                  employee = d.name,
                                  procedure = e.name,
                                  procedure_price = e.price,
                                  //client_id = a.clientId
                                  client = c.name
                              }).OrderByDescending(x => x.check_in).ToList();
                return Ok(query1);
            }
            var query = (from a in _context.TB_Scheduling
                         join b in _context.TB_Login on a.employeeId equals b.IdLogin
                         join c in _context.TB_People on a.clientId equals c.idPeople
                         join d in _context.TB_People on b.peopleId equals d.idPeople
                         join e in _context.TB_Procedure on a.procedureId equals e.idProcedure

                         select new
                         {
                             id_scheduling = a.idScheduling,
                             status_scheduling = Convert.ToString(a.status),//a.status,
                             check_in = a.checkIn,
                             check_out = a.checkOut,
                             //employee_id = a.employeeId,
                             employee = d.name,
                             procedure = e.name,
                             procedure_price = e.price,
                             //client_id = a.clientId
                             client = c.name
                         }).OrderByDescending(x => x.check_in).ToList();

            return Ok(query);
        }
        #endregion

        #region Get of range
        [HttpGet("range")]
        public IActionResult ReadRange([FromQuery] DateTime startDate, DateTime? endDate)
        {
            if(endDate < startDate){
                return ValidationProblem(DefaultMessages.DateValidationProblem, null, 422,"Valide as datas, endDate não pode ser menor que startDate."); 
            }
            if(endDate != null && endDate > startDate)
            {
                var consultaRange = (from a in _context.TB_Scheduling
                         join b in _context.TB_Login on a.employeeId equals b.IdLogin
                         join c in _context.TB_People on a.clientId equals c.idPeople
                         join d in _context.TB_People on b.peopleId equals d.idPeople
                         join e in _context.TB_Procedure on a.procedureId equals e.idProcedure
                            where a.checkIn >= startDate && a.checkIn <= endDate
                         select new
                         {
                             id_scheduling = a.idScheduling,
                             status_scheduling = Convert.ToString(a.status),//a.status,
                             check_in = a.checkIn,
                             check_out = a.checkOut,
                             //employee_id = a.employeeId,
                             employee = d.name,
                             procedure = e.name,
                             procedure_price = e.price,
                             //client_id = a.clientId
                             client = c.name
                         }).OrderByDescending(x => x.check_in).ToList();
                return Ok(consultaRange);
            }
            if (endDate != null && endDate == startDate)
            {
                startDate = startDate.AddHours(23).AddMinutes(59);
                var ConsultaIguais = (from a in _context.TB_Scheduling
                         join b in _context.TB_Login on a.employeeId equals b.IdLogin
                         join c in _context.TB_People on a.clientId equals c.idPeople
                         join d in _context.TB_People on b.peopleId equals d.idPeople
                         join e in _context.TB_Procedure on a.procedureId equals e.idProcedure
                            where a.checkIn >= endDate && a.checkIn <= startDate
                         select new
                         {
                             id_scheduling = a.idScheduling,
                             status_scheduling = Convert.ToString(a.status),//a.status,
                             check_in = a.checkIn,
                             check_out = a.checkOut,
                             //employee_id = a.employeeId,
                             employee = d.name,
                             procedure = e.name,
                             procedure_price = e.price,
                             //client_id = a.clientId
                             client = c.name
                         }).OrderByDescending(x => x.check_in).ToList();
                return Ok(ConsultaIguais);
            }
            var query = (from a in _context.TB_Scheduling
                         join b in _context.TB_Login on a.employeeId equals b.IdLogin
                         join c in _context.TB_People on a.clientId equals c.idPeople
                         join d in _context.TB_People on b.peopleId equals d.idPeople
                         join e in _context.TB_Procedure on a.procedureId equals e.idProcedure
                            where a.checkIn >= startDate
                         select new
                         {
                             id_scheduling = a.idScheduling,
                             status_scheduling = Convert.ToString(a.status),//a.status,
                             check_in = a.checkIn,
                             check_out = a.checkOut,
                             //employee_id = a.employeeId,
                             employee = d.name,
                             procedure = e.name,
                             procedure_price = e.price,
                             //client_id = a.clientId
                             client = c.name
                         }).OrderByDescending(x => x.check_in).ToList();
            return Ok(query);

        }
        #endregion

        [HttpGet("v1")]
        public IActionResult Read()
        {
            if (!ModelState.IsValid)
                return BadRequest("deuruim");
            else
            {
                var query = (from a in _context.TB_Scheduling
                             join b in _context.TB_Login on a.employeeId equals b.IdLogin
                             join c in _context.TB_People on a.clientId equals c.idPeople
                             join d in _context.TB_People on b.peopleId equals d.idPeople
                             join e in _context.TB_Procedure on a.procedureId equals e.idProcedure

                             select new
                             {
                                 id_scheduling = a.idScheduling,
                                 status_scheduling = Convert.ToString(a.status),//a.status,
                                 check_in = a.checkIn,
                                 check_out = a.checkOut,
                                 //employee_id = a.employeeId,
                                 employee = d.name,
                                 procedure = e.name,
                                 procedure_price = e.price,
                                 //client_id = a.clientId
                                 client = c.name
                             }).OrderByDescending(x => x.check_in).ToList();

                return Ok(query);
            }
        }


    }
}

