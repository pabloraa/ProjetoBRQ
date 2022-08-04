using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.DataBaseConection;
using WebRepositories.Interfaces;
using WebServiceApi.Interfaces;
using WebServiceApi.Respositoty;
using WebServiceApi.Services;
using MySql.Data.MySqlClient;


namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("MeuBancoDeDados"));            
            services.AddControllers();

            services.AddDbContext<ApiContext>(opt => opt.UseSqlServer(@"Server = DESKTOP-10HCQPV\SQLEXPRESS; Database = ExemploBD; Trusted_Connection = True;"));
            services.AddScoped<IRepositoryConta, RepositoryConta>();
            services.AddScoped<IContaService, ContaService>();

            services.AddScoped<IRepositoryTransacao, RepositoryTransacao>();
            services.AddScoped<ITransacaoService, TransacaoService>();

            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IRepositoryPessoa, RepositoryPessoa>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
