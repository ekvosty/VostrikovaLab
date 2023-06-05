using VostrikovaLab.Enums;
using VostrikovaLab.Extensions;
using VostrikovaLab.Interfaces;
using VostrikovaLab.Models;

namespace VostrikovaLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            switch (builder.Configuration["Storage:Type"]!.ToStorageEnum())
            {
                case StorageEnum.MemCache:
                    builder.Services.AddSingleton<IStorage<BookModel>, MemCache>();
                    break;
                case StorageEnum.FileStorage:
                    builder.Services.AddSingleton<IStorage<BookModel>>(
                        x => new FileStorage(builder.Configuration["Storage:FileStorage:Filename"], int.Parse(builder.Configuration["Storage:FileStorage:FlushPeriod"])));
                    break;
                default:
                    throw new IndexOutOfRangeException($"Тип хранилища '{builder.Configuration["Storage:Type"]}' неизвестен");
            }



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}