using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Repositories;
using WPFApp_LibraryManager.Services;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager
{
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<IAuthorService, AuthorService>();
                    services.AddTransient<IBookService, BookService>();
                    services.AddTransient<IBookValidator, BookValidator>();
                    services.AddTransient<ICategoryService, CategoryService>();
                    services.AddTransient<ICategoryValidator, CategoryValidator>();
                    services.AddTransient<IPublisherService, PublisherService>();
                    services.AddTransient<IPublisherValidator, PublisherValidator>();
                    services.AddTransient<IAuthorRepository, AuthorRepository>();
                    services.AddTransient<IAuthorValidator, AuthorValidator>();
                    services.AddTransient<IBookRepository, BookRepository>();
                    services.AddTransient<ICategoryRepository, CategoryRepository>();
                    services.AddTransient<IPublisherRepository, PublisherRepository>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}