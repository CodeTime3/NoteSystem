using MySql.Data.MySqlClient;

namespace NoteSystemWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = "server=localhost;uid=root;pwd=CT22d03p06;database=notesystem";
            MySqlConnection connection = new MySqlConnection(connectionString);
            GeneralCrud generalCrud = new GeneralCrud(connection);

            builder.Services.AddSingleton(generalCrud);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(
                    options => 
                    {
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                        options.SlidingExpiration = true;
                        options.AccessDeniedPath = "/Forbidden/";
                    }
                );

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}