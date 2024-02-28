using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WPF.Application.Core;
using WPF.Application.MVVM.Model.Interfaces;
using WPF.Application.MVVM.View;
using WPF.Application.MVVM.ViewModel;
using WPF.Application.Services;

namespace WPF.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<ItemsViewModel>();
            services.AddSingleton<DetallePokemonViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IProductDataService, ProductoDataService>();
            services.AddSingleton<IPokemonDataService, PokemonDataService>();

            services.AddSingleton<Func<Type, BaseViewModel>>(serviceProvider => viewModelType => (BaseViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>(); 
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
