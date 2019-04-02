using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;

namespace BlazorSignalR.Server {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddSignalR();
            services.AddSingleton<ProducerService>();

#if RUNATSERVER
            
            Client.Startup.ConfigureServicesForDebug = x => {
                x.AddSingleton(new HttpClient() {
                    BaseAddress = new Uri(@"http://localhost:8500/")
                });
            };
            services.AddServerSideBlazor<Client.Startup>();
#endif
            services.AddResponseCompression(options => {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });

            var provider=services.BuildServiceProvider();
            var prov=provider.GetRequiredService<ProducerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseResponseCompression();
            app.UseDeveloperExceptionPage();
            

            app.UseMvc(routes => {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });
            app.UseSignalR(x => x.MapHub<MessageHub>("/message"));
#if RUNATSERVER
            app.UseServerSideBlazor<Client.Startup>();
#else
            app.UseBlazor<Client.Startup>();
#endif


        }
    }
}
