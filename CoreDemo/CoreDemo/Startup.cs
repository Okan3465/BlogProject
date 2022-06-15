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

           services.AddSession(); //oturum ekleme sayfasý.Login controllerda oluþturduðum session iþlemi için startup'da ConfigureService  ve add.session iþlemi yaptýk.



            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                              .RequireAuthenticatedUser()        //Authorize iþlemini proje seviyesine taþýmak için bu iþlemleri yapýyoruz
                              .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddMvc();                     //bu satýr ve bir alt satýrdaki iþlemi click edildiðind egelmeyen sayfalarý getirmek                                        için yapýyoruz .Bu iþlemi uygulayýnca sadece Login/Index sayfasýna götürüyor bütün                                               deðerleri.
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

            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1","?Code={0}"); //null döenbilir o yüzden soru iþareti býraktým.Buradaki ismin controllerdaki isimle ayný olmasý gerekir .Hata sayfasýný getirmek için kullandým
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();  // AddSession için yapýlan iþlemlerden biri 
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
