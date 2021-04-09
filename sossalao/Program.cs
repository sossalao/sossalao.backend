using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using sossalao.Core.Data;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sossalao
{
	public class Program
	{
		public static void Main(string[] args)
		{
            var ambiente = BuildWebHost(args);
            using (var escopo = ambiente.Services.CreateScope())
            {
                var servico = escopo.ServiceProvider;
                try
                {
                    var contex = servico.GetRequiredService<DataBaseContext>();
                    InitDB.Init(contex);
                    //contex.Database.EnsureCreated();
                    // InicializarBanco.Iniciar(contex);
                }
                catch (Exception e)
                {
                    var logger = servico.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Ocorreu um erro enquanto os dados foram enviados");
                }
            }
            ambiente.Run();
            //CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>()
               .Build();

        //private static object BuildWebHost(string[] args)
        //{
        //	throw new NotImplementedException();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //	WebHost.CreateDefaultBuilder(args)
        //		.UseStartup<Startup>();
    }
}
