using LogProxyAPI.Helper;
using LogProxyAPI.Services.Interface;
using LogProxyAPI.Services.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThirdPartyAPI.ACL.Services;

namespace LogProxyAPI
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(ILogProxyService), new LogProxyService(new AirTable())));
            services.Add(new ServiceDescriptor(typeof(ILoginService), new LoginService(_configuration)));
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
