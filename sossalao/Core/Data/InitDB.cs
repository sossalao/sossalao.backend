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

#region Product's
			var ProductShampoo = new Product()
			{
				name = "Xampú RankaKaspa",
				make = "Kikabelo",
				description = "Xampú anticaspa profissional."
			};
			dbContext.TB_Product.Add(ProductShampoo);

			var ProductEstetica = new Product()
			{
				name = "Esfoliante Intenso",
				make = "EsfoliaPro",
				description = "Esfoliante profissional."
			};
			dbContext.TB_Product.Add(ProductEstetica);
			
			var ProductDesmaquilante = new Product()
			{
				name = "Água Micelar 5 em 1",
				make = "L'Oréal Paris",
				description = "Água micelar de l'oréal paris é a solução de limpeza facial que foi especialmente desenvolvida para limpar a pele, "+
				"e sua embalagem de 100ml é perfeita para guardar em sua necessaire e levar em viagens. Com o alto poder de limpeza e purificação, sua fórmula "+
				"capta e elimina as impurezas e maquiagem, revelando uma pele perfeitamente limpa, reequilibrada e suave sem agredi-la. Feita para todos os tipos de pele."
			};
			dbContext.TB_Product.Add(ProductDesmaquilante);
			var ProductAcetona = new Product()
			{
				name = "Removedor de Esmaltes Zero Acetona Aroma Uva",
				make = "Beira Alta",
				description = "Removedor sem acetona, vit e e b5, óleos vegetais, cálcio, dermatologicamente testado. Remove sem esbranquiçar. Hipoalegênico. Remove esmaltes sem esbranquiçar com aroma de uva. "
			};
			dbContext.TB_Product.Add(ProductAcetona);
#endregion

#region Supplier
			var Supp = new Supplier()
			{
				supplierName = "Perfurmaria Amaral LTDA",
				phoneNumber = "1127249384",
				description = "Perfurmaria mais barata de produtos em geral."
			};
			dbContext.TB_Supplier.Add(Supp);
#endregion

#region Stock
			var StockAcetona = new Stock()
			{
				productId = ProductAcetona.idProduct,
				quantity = 240,
				typeProduct = Utils.TypeProduct.Condicionador,
				supplierId = Supp.idSupplier
			};
			dbContext.TB_Stock.Add(StockAcetona);
			var StockEstetica = new Stock()
			{
				productId = ProductEstetica.idProduct,
				quantity = 80,
				typeProduct = Utils.TypeProduct.Mascara,
				supplierId = Supp.idSupplier
			};
			dbContext.TB_Stock.Add(StockEstetica);
			var StockShampoo = new Stock()
			{
				productId = ProductShampoo.idProduct,
				quantity = 240,
				typeProduct = Utils.TypeProduct.Xampu,
				supplierId = Supp.idSupplier
			};
			dbContext.TB_Stock.Add(StockShampoo);

			var StockDesmaquilante = new Stock()
			{
				productId = ProductDesmaquilante.idProduct,
				quantity = 55,
				typeProduct = Utils.TypeProduct.Desmaquilante,
				supplierId = Supp.idSupplier
			};
			dbContext.TB_Stock.Add(StockDesmaquilante);
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

#region Procedure <-> Stock
		//Relação de produtos usados em 'Hidratação Capilar'. 
			var StockProcedureHair = new StockAndProcedure()
			{
				stockId = StockShampoo.idStock,
				procedureId = ProcedureHair.idProcedure
				,requiredQuantity = 2 
			};
			dbContext.TB_StockAndProcedure.Add(StockProcedureHair);

			var StockProcedureHairTWO = new StockAndProcedure()
			{ 
				stockId = StockAcetona.idStock, 
				procedureId = ProcedureHair.idProcedure, 
				requiredQuantity = 10 
			};
			dbContext.TB_StockAndProcedure.Add(StockProcedureHairTWO);
			
			var StockProcedureHairThree = new StockAndProcedure()
			{ 
				stockId = StockEstetica.idStock, 
				procedureId = ProcedureHair.idProcedure, 
				requiredQuantity = 5 
			};
			dbContext.TB_StockAndProcedure.Add(StockProcedureHairThree);
		//Relação de produtos usados em 'Limpeza de pele'.
			var StockProcedureEstetica = new StockAndProcedure()
			{
				stockId = StockDesmaquilante.idStock,
				procedureId = ProcedureEstetica.idProcedure
				,requiredQuantity = 1 
			};
			dbContext.TB_StockAndProcedure.Add(StockProcedureEstetica);

			var StockProcedureEsteticaTWO = new StockAndProcedure()
			{ 
				stockId = StockEstetica.idStock, 
				procedureId = ProcedureEstetica.idProcedure, 
				requiredQuantity = 1 
			};
			dbContext.TB_StockAndProcedure.Add(StockProcedureEsteticaTWO);
#endregion

#region Combo

			var Combo = new Combo()
			{
				name = "S.O.S Combo",
				description = "Podemos já vislumbrar o modo pelo qual a adoção de políticas " +
				"descentralizadoras deve passar por modificações independentemente das formas de ação.",
				price = 69.90M				
			};
			dbContext.TB_Combo.Add(Combo);
#endregion

#region  Procedure <-> Combo

			var Relation_ComboProcedure00 = new ComboAndProcedure(){comboId = Combo.idCombo, procedureId = ProcedureHair.idProcedure};
			dbContext.TB_ComboAndProcedure.Add(Relation_ComboProcedure00);
			
			var Relation_ComboProcedure01 = new ComboAndProcedure() 
			{ 
				comboId = Combo.idCombo, 
				procedureId = ProcedureEstetica.idProcedure 
			};
			dbContext.TB_ComboAndProcedure.Add(Relation_ComboProcedure01);
#endregion

#region Sale
			var Sale = new Sale()
			{
				comboId = Combo.idCombo,
				amount = Combo.price,
				clientId = PeopleClient.idPeople,
				procedureId = ProcedureHair.idProcedure
			};
			dbContext.TB_Sale.Add(Sale);
#endregion

#region Scheduling
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
#endregion

			dbContext.SaveChanges();
		}
	}
}
