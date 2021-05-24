using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using sossalao.Core.Models;
using sossalao.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Data
{
	public class DataBaseContext : DbContext
	{
		public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) {}
		public DbSet<People> TB_People { get; set; }
		public DbSet<Combo> TB_Combo { get; set; }
		public DbSet<Stock> TB_Stock { get; set; }
		public DbSet<Login> TB_Login { get; set; }
		public DbSet<Procedure> TB_Procedure { get; set; }
		public DbSet<ComboAndProcedure> TB_ComboAndProcedure { get; set; }

		public DbSet<Product> TB_Product { get; set; }
		public DbSet<Sale> TB_Sale { get; set; }
		public DbSet<Scheduling> TB_Scheduling { get; set; }
		public DbSet<StockAndProcedure> TB_StockAndProcedure { get; set; }
		public DbSet<Supplier> TB_Supplier { get; set; }
		protected override void OnModelCreating(ModelBuilder mb)
		{
			#region ToTable and Haskey of all Model's.
			//mb.Entity<Combo>().ToTable("TB_Combo").HasKey(a => a.idCombo);
			//mb.Entity<Login>().ToTable("TB_Login").HasKey(a => a.IdLogin);
			//mb.Entity<People>().ToTable("TB_People").HasKey(a => a.idPeople);
			//mb.Entity<Procedure>().ToTable("TB_Procedure").HasKey(a => a.idProcedure);
			//mb.Entity<Product>().ToTable("TB_Product").HasKey(a => a.idProduct);
			//mb.Entity<Sale>().ToTable("TB_Sale").HasKey(a => a.idSale);
			//mb.Entity<Scheduling>().ToTable("TB_Scheduling").HasKey(a => a.idScheduling);
			//mb.Entity<Login>().HasOne(x => x.Scheduling).WithMany(x => x.Login).HasForeignKey
			//mb.Entity<Scheduling>().HasOne<Login>().WithMany().HasForeignKey(p => p.FuncId);
			//mb.Entity<People>().HasOne(b => b.Login).WithOne(i => i.People).HasForeignKey<Login>(b => b.peopleId);
			//mb.Entity<Stock>().ToTable("TB_Stock").HasKey(a => a.idStock);
			//mb.Entity<Supplier>().ToTable("TB_Supplier").HasKey(a => a.idSupplier);
			#endregion


			//		#region HasConversion enum to int of all Model's.
			//mb.Entity<People>().Property(e => e.typePeople).HasConversion(x => (int)x, x => (TypePeople)x);
			//		mb.Entity<Login>().Property(e => e.typeEmployee).HasConversion(x => (int)x, x => (TypeEmployee)x);
			//		mb.Entity<Login>().Property(e => e.typeArea).HasConversion(x => (int)x, x => (TypeArea)x);
			//		mb.Entity<Login>().Property(e => e.accessLevel).HasConversion(x => (int)x, x => (AccessLevel)x);
			//		mb.Entity<Procedure>().Property(e => e.typeArea).HasConversion(x => (int)x, x => (TypeArea)x);

			//		#endregion


			//		#region HasForeignKey of all Model's.
			//		//Combo XXXXXXXXXX
			//		//Login 
			//		mb.Entity<People>().HasOne(b => b.Login).WithOne(i => i.People).HasForeignKey<Login>(b => b.peopleId);
			////		mb.Entity<People>(
			////ob =>
			////{
			////	ob.OwnsOne(
			////		o => o.Login,
			////		sa =>
			////		{
			////			sa.Property(c => c.IdLogin).IsRequired();
			////			sa.Property(p => p.peopleId).IsRequired();
			////		});



			////});
			////		mb.Entity<People>()
			////	.HasOne(a => a.Login).WithOne(b => b.People)
			////	.HasForeignKey<Login>(e => e.peopleId);
			//		//mb.Entity<Combo>().HasOne<Procedure>().WithMany().HasForeignKey(p => p.procudures);
			//		//mb.Entity<Combo>().HasMany<Procedure>().().HasForeignKey(p => p.procudures);

			//		//Scheduling ForeignKey 
			//		mb.Entity<Scheduling>().HasOne<People>().WithMany().HasForeignKey(p => p.clientId);
			//		mb.Entity<Scheduling>().HasOne<People>().WithMany().HasForeignKey(p => p.employeeId);
			//		mb.Entity<Scheduling>().HasOne(b => b.Sale).WithOne(i => i.Scheduling).HasForeignKey<Sale>(b => b.idSale);



			//		#endregion

			//mb.Entity<Combo>()
			//.HasMany(b => b.procudures)
			//.WithOne();
			//var splitStringConverter = new ValueConverter<IEnumerable<string>, string>(v => string.Join(";", v), v => v.Split(new[] { ';' }));
			//mb.Entity<Combo>().Property(nameof(Combo.procedure)).HasConversion(splitStringConverter);

			//		mb.Entity<Combo>()
			//.Property(e => e.procedures01)
			//.HasConversion(
			//	v => JsonSerializer.Serialize(v, null),
			//	v => JsonSerializer.Deserialize<List<Procedure>>(v, null),
			//	new ValueComparer<IList<Procedure>>(
			//		(c1, c2) => c1.SequenceEqual(c2),
			//		c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
			//		c => (IList<Procedure>)c.ToList()));

			//mb.Entity<Combo>().HasMany(x => x.procedures).WithRequired().HasForeignKey(con => con.EndCityId);
			//mb.Entity<ProcedureAndStock>().ToTable("TB_ProcedureAndStock").HasOne<Procedure>(pd => pd.Procedure).WithMany(p => p.ProcedureAndStocks).HasForeignKey(pd => pd.procedureId);
			//mb.Entity<ProcedureAndStock>().ToTable("TB_ProcedureAndStock").HasOne<Stock>(pt => pt.Stock).WithMany(p => p.ProcedureAndStocks).HasForeignKey(pt => pt.stockId);
			//mb.Entity<StockAndProcedure>().ToTable("TB_StockAndProcedure").HasOne<Stock>(pt => pt.Stock).WithMany(p => p.stoqPro).HasForeignKey(pt => pt.StockId);

		}
	}
}
