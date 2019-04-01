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

#if RUNATSERVER
            services.AddServerSideBlazor<Client.Startup>();
            Client.Startup.ConfigureServicesForDebug = x => {
                x.AddSingleton(new HttpClient() {
                    BaseAddress = new Uri(@"http://localhost:8500/")
                });
            };
#endif
            services.AddResponseCompression(options => {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseResponseCompression();
            app.UseDeveloperExceptionPage();
            app.UseSignalR(x => x.MapHub<MessageHub>("/message"));

            app.UseMvc(routes => {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });
#if RUNATSERVER
           app.UseServerSideBlazor<Client.Startup>();
#else
            app.UseBlazor<Client.Startup>();
#endif

        }
    }
}
