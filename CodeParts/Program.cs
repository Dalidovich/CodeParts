using CodeParts.Data;
using CodeParts.Data.Interfaces;
using CodeParts.Data.Repositories;
using CodeParts.Data.TableModel;
using CodeParts.Service.Implementations;
using CodeParts.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace CodeParts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(
                opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("myDb"))
                );

            builder.Services.AddScoped<ICodeRepository, CodeRepository>();
            builder.Services.AddScoped<ICodeService, CodeService>();

            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/LogIn");
                    opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/LogIn");
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            if (false)
            {
                using var scope = app.Services.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
            }

            app.Run();
        }
    }
}