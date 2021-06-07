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
		public DbSet<Login> TB_Login { get; set; }
		public DbSet<Procedure> TB_Procedure { get; set; }
		public DbSet<Scheduling> TB_Scheduling { get; set; }
		protected override void OnModelCreating(ModelBuilder mb){}
	}
}
