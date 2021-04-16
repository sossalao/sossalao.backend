using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sossalao.Core.Models;

namespace sossalao.Core.Data
{
	public class InitDB
	{
		public static void Init(DataBaseContext dbContext)
		{
			dbContext.Database.EnsureCreated();
			if (dbContext.TB_People.Any()) { return; }

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

			var ProductHair = new Product()
			{
				name = "Xampú RankaKaspa",
				make = "Kikabelo",
				description = "Xampú anticaspa profissional."
			};
			dbContext.TB_Product.Add(ProductHair);

			var ProductEstetica = new Product()
			{
				name = "Esfoliante Intenso",
				make = "EsfoliaPro",
				description = "Esfoliante profissional."
			};
			dbContext.TB_Product.Add(ProductEstetica);

			var Supp = new Supplier()
			{
				supplierName = "Perfurmaria Amaral LTDA",
				phoneNumber = "1127249384",
				description = "Perfurmaria mais barata de produtos em geral."
			};
			dbContext.TB_Supplier.Add(Supp);
			var Stock = new Stock()
			{
				productId = ProductHair.idProduct,
				quantity = 240,
				typeProduct = Utils.TypeProduct.Xampu,
				supplierId = Supp.idSupplier
			};
			dbContext.TB_Stock.Add(Stock);
			var Stock2 = new Stock()
			{
				productId = ProductEstetica.idProduct,
				quantity = 80,
				typeProduct = Utils.TypeProduct.Mascara,
				supplierId = Supp.idSupplier
			};
			dbContext.TB_Stock.Add(Stock2);
			//var Stock00 = new Stock()
			//{
			//	productId = ProductHair.idProduct,
			//	quantity = 240,
			//	typeProduct = Utils.TypeProduct.Xampu,
			//	supplierId = Supp.idSupplier
			//};
			//dbContext.TB_Stock.Add(Stock00);

			//var Stock01 = new Stock()
			//{
			//	productId = ProductEstetica.idProduct,
			//	quantity = 80,
			//	typeProduct = Utils.TypeProduct.Mascara,
			//	supplierId = Supp.idSupplier
			//};
			//dbContext.TB_Stock.Add(Stock01);



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

			//var Relation_ProcedureStock00 = new StockAndProcedure()
			//{
			//	//StockId = Stock00.idStock,
			//	procedureId = ProcedureHair.idProcedure
			//	//,requiredQuantity = 2 
			//};
			//dbContext.TB_StockAndProcedure.Add(Relation_ProcedureStock00);

			//var Relation_ProcedureStock01 = new StockAndProcedure()
			//{
			//	//StockId = Stock01.idStock,
			//	procedureId = ProcedureEstetica.idProcedure
			//	//,requiredQuantity = 1 
			//};
			//dbContext.TB_StockAndProcedure.Add(Relation_ProcedureStock01);

			var Combo = new Combo()
			{
				name = "S.O.S Combo",
				description = "Podemos já vislumbrar o modo pelo qual a adoção de políticas " +
				"descentralizadoras deve passar por modificações independentemente das formas de ação.",
				price = 69.90M				
			};
			dbContext.TB_Combo.Add(Combo);

			var Relation_ComboProcedure00 = new ComboAndProcedure(){comboId = Combo.idCombo, procedureId = ProcedureHair.idProcedure};
			dbContext.TB_ComboAndProcedure.Add(Relation_ComboProcedure00);
			
			var Relation_ComboProcedure01 = new ComboAndProcedure() 
			{ 
				comboId = Combo.idCombo, 
				procedureId = ProcedureEstetica.idProcedure 
			};
			dbContext.TB_ComboAndProcedure.Add(Relation_ComboProcedure01);

			var xpto = new StockAndProcedure()
			{ stockId = Stock.idStock, procedureId = ProcedureHair.idProcedure , requiredQuantity = 10 };
			dbContext.TB_StockAndProcedure.Add(xpto);
			var xptoo1 = new StockAndProcedure()
			{ stockId = Stock2.idStock, procedureId = ProcedureHair.idProcedure, requiredQuantity = 5 };
			dbContext.TB_StockAndProcedure.Add(xptoo1);
			var xpto1 = new StockAndProcedure()
			{ stockId = Stock2.idStock, procedureId = ProcedureEstetica.idProcedure, requiredQuantity = 1 };
			dbContext.TB_StockAndProcedure.Add(xpto1);

			var Sale = new Sale()
			{
				comboId = Combo.idCombo,
				amount = Combo.price,
				clientId = PeopleClient.idPeople,
				procedureId = ProcedureHair.idProcedure
			};
			dbContext.TB_Sale.Add(Sale);

			var Scheduling = new Scheduling()
			{
				checkIn = new DateTime(2021, 08, 20, 16, 30, 0),
				checkOut = new DateTime(2021, 08, 20, 18, 00, 0),
				saleId = Sale.idSale,
				employeeId = Login.IdLogin,
				clientId = PeopleClient.idPeople,
				status = Utils.StatusScheduling.Marcado
			};
			dbContext.TB_Scheduling.Add(Scheduling);

			dbContext.SaveChanges();
		}
	}
}
