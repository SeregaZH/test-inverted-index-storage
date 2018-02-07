using System.IO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TIIS.Project.API.Controllers;
using TIIS.Project.API.Extensions;

namespace TIIS.Project.API
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

            services.AddSingleton<IDirectoryProvider>(s =>
                new SimpleFSDirectoryProvider(s.GetService<IHostingEnvironment>()));
            services.AddSingleton<IIndexWriter>(s =>
            {
                return new DocumentWriter(
                    new IndexWriter(s.GetService<IDirectoryProvider>().Provide(),
                    new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29),
                    IndexWriter.MaxFieldLength.UNLIMITED));
            });
            services.AddSingleton<IIndexSearcher>(s => new DocumentSearcher(new IndexSearcher(s.GetService<IDirectoryProvider>().Provide(), true)));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureLucene(env);
            app.UseMvc();
        }
    }
}
