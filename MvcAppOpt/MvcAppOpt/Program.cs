
using MvcAppOpt;

namespace MvcApp
{
    class Program
    {
        static void Main()
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddControllersWithViews(ops => ops.ModelBinderProviders.Insert(0, DecimalModelBinderProvider.Instance));

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}