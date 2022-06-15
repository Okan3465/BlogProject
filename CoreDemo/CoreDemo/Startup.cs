using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo
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
            services.AddControllersWithViews();

           services.AddSession(); //oturum ekleme sayfas�.Login controllerda olu�turdu�um session i�lemi i�in startup'da ConfigureService  ve add.session i�lemi yapt�k.



            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                              .RequireAuthenticatedUser()        //Authorize i�lemini proje seviyesine ta��mak i�in bu i�lemleri yap�yoruz
                              .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddMvc();                     //bu sat�r ve bir alt sat�rdaki i�lemi click edildi�ind egelmeyen sayfalar� getirmek                                        i�in yap�yoruz .Bu i�lemi uygulay�nca sadece Login/Index sayfas�na g�t�r�yor b�t�n                                               de�erleri.
            services.AddAuthentication(           
                CookieAuthenticationDefaults.AuthenticationScheme
                )
                .AddCookie(x =>
                {
                    x.LoginPath = "/Login/Index";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1","?Code={0}"); //null d�enbilir o y�zden soru i�areti b�rakt�m.Buradaki ismin controllerdaki isimle ayn� olmas� gerekir .Hata sayfas�n� getirmek i�in kulland�m
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();  // AddSession i�in yap�lan i�lemlerden biri 
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
