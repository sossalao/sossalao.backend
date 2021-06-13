using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sossalao.Core.Models;
using sossalao.Core.Utils;

namespace sossalao.Core.Data
{
	public class InitDB
	{
		
		public static void Init(DataBaseContext dbContext)
		{
            Security security = new Security();
			dbContext.Database.EnsureCreated();
			if (dbContext.TB_People.Any()) { return; }

#region People
			var PeopleClient = new People()
			{
				name = "Agatha Lorena Gomes",
				email = "aagathalorenagomes@pop.com.br",
				phoneNumber = "34993896109",
				typePeople = Utils.TypePeople.Client
			};
			dbContext.TB_People.Add(PeopleClient);

			var PeopleEmployee = new People()
			{
				name = "Mariana Souza",
				email = "mariana.souza98@live.com",
				phoneNumber = "5511961235780",
				typePeople = Utils.TypePeople.Employee
			};
			dbContext.TB_People.Add(PeopleEmployee);
			var Dev = new People()
			{
				name = "Developer",
				email = "developer@live.com",
				phoneNumber = "5511961235780",
				typePeople = Utils.TypePeople.Employee
			};
			dbContext.TB_People.Add(Dev);
#endregion

#region Login
			var Login = new Login()
			{
				peopleId = PeopleEmployee.idPeople,
				user = "marinasouza",
				password = "!Mariana30#",
				accessLevel = Utils.AccessLevel.Master,
				typeEmployee = Utils.TypeEmployee.Efetivo,
				typeArea = Utils.TypeArea.CabeleireiroHairStylist
			};
			dbContext.TB_Login.Add(Login);

			var LoginDev = new Login()
			{
				peopleId = PeopleEmployee.idPeople,
				user = "hrqlp",
				password = security.cryptopass("devsos21!"),
				accessLevel = Utils.AccessLevel.Master,
				typeEmployee = Utils.TypeEmployee.Efetivo,
				typeArea = Utils.TypeArea.Esteticismo
			};
			dbContext.TB_Login.Add(LoginDev);
#endregion




#region Procedure
			var ProcedureHair = new Procedure()
			{
				name = "Hidratação Capilar",
				description = "Esse procedimento transforma seu cabelo seboso e fedido em um cabelo lindo e cheiroso!!!",
				estimitedTime = new TimeSpan(0, 40, 0),
				price = 49.90M,
				typeArea = Utils.TypeArea.CabeleireiroHairStylist
			};
			dbContext.TB_Procedure.Add(ProcedureHair);

			var ProcedureEstetica = new Procedure()
			{
				name = "Limpeza de pele",
				description = "Esse procedimento transforma sua pele sebosa e fedida em uma pele linda e cheirosa!!!",
				estimitedTime = new TimeSpan(0, 30, 0),
				price = 39.90M,
				typeArea = Utils.TypeArea.Esteticismo
			};
			dbContext.TB_Procedure.Add(ProcedureEstetica);
#endregion





#region Scheduling
			var Scheduling = new Scheduling()
			{
				checkIn = new DateTime(2021, 08, 20, 16, 30, 0),
				checkOut = new DateTime(2021, 08, 20, 18, 00, 0),
				procedureId = ProcedureHair.idProcedure,
				employeeId = Login.IdLogin,
				clientId = PeopleClient.idPeople,
				status = Utils.StatusScheduling.EmAberto
			};
			dbContext.TB_Scheduling.Add(Scheduling);
			var Scheduling2 = new Scheduling()
			{
				checkIn = new DateTime(2021, 09, 20, 16, 30, 0),
				checkOut = new DateTime(2021, 09, 20, 18, 00, 0),
				procedureId = ProcedureHair.idProcedure,
				employeeId = Login.IdLogin,
				clientId = PeopleClient.idPeople,
				status = Utils.StatusScheduling.EmAberto
			};
			dbContext.TB_Scheduling.Add(Scheduling2);
			var Scheduling1 = new Scheduling()
			{
				checkIn = new DateTime(2021, 08, 20, 16, 30, 0),
				checkOut = new DateTime(2021, 08, 20, 18, 00, 0),
				procedureId = ProcedureHair.idProcedure,
				employeeId = Login.IdLogin,
				clientId = PeopleEmployee.idPeople,
				status = Utils.StatusScheduling.EmAberto
			};
			dbContext.TB_Scheduling.Add(Scheduling1);
			#endregion

			dbContext.SaveChanges();
		}
	}
}
