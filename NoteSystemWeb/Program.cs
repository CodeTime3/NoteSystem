namespace NoteSystemWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("MyDB");
            MySqlConnection connection = new MySqlConnection(connectionString);
            GetNameTableAttribute getNameTable = new GetNameTableAttribute();
            StringQueriesBuilderParameter stringQueriesBuilder = new StringQueriesBuilderParameter();
            IGeneralCrud generalCrud = new GeneralCrud(connection, getNameTable, stringQueriesBuilder);

            builder.Services.AddScoped(sp => generalCrud);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

			builder.Services.AddDistributedMemoryCache();

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(
                    options => 
                    {
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                        options.SlidingExpiration = true;
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
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}