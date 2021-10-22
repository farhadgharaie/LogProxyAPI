using LogProxyAPI.Helper;
using LogProxyAPI.Services.Interface;
using LogProxyAPI.Services.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThirdPartyAPI.ACL.Services;

namespace LogProxyAPI
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(ILogProxyService), new LogProxyService(new AirTable())));
            services.AddControllers();
            services.AddAuthentication("BasicAuth")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuth", null);
                
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }
    }
}
