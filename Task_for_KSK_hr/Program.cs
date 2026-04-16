
using Microsoft.Extensions.Options;
using System.Reflection;
using Task_for_KSK_hr.Monitors;
using Task_for_KSK_hr.Repositories;
using Task_for_KSK_hr.Services;

namespace Task_for_KSK_hr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try {
                
                var builder = WebApplication.CreateBuilder(args);


                //var sp = builder.Services.BuildServiceProvider();
                

                builder.Services.AddControllers();


                
                builder.Services.AddSingleton<IItemRepository, ItemRepository>();
                builder.Services.AddSingleton<IPurchaseRepository, PurchaseRepository>();

                builder.Services.AddScoped<IItemService, ItemService>();
                builder.Services.AddScoped<IPurchaseService, PurchaseService>();
                builder.Services.AddSingleton<ITopCategoryMonitor, TopCategoryMonitor>();

                builder.Services.AddSingleton<IOptionsMonitor<TopCategoryOptions>>(sp => sp.GetRequiredService<ITopCategoryMonitor>());

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();


                var app = builder.Build();



                app.UseHttpsRedirection();


                app.MapControllers();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.Run();

            }
            catch (ReflectionTypeLoadException ex)
{
                Console.WriteLine("Ошибка загрузки типов:");
                foreach (var e in ex.LoaderExceptions)
                {
                    Console.WriteLine(e.Message);
                }
                throw;
            }
        }
    }
}
