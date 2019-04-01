using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorSignalR.Client {
    
    public class Startup {
        public static Action<IServiceCollection> ConfigureServicesForDebug { get; set; }
        public void ConfigureServices(IServiceCollection services) {
            ConfigureServicesForDebug?.Invoke(services);
        }

        public void Configure(IBlazorApplicationBuilder app) {
            app.AddComponent<App>("app");
        }
    }
}
