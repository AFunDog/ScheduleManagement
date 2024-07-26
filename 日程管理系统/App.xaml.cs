using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Messaging;
using CoreServices.Localization;
using CoreServices.Setting;
using CoreServices.WinUI.Contracts;
using CoreServices.WinUI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using 日程管理系统.Contracts;
using 日程管理系统.Core.Contracts;
using 日程管理系统.Services;
using 日程管理系统.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static App Instance => (Current as App)!;
        public static IMessenger Messenger => WeakReferenceMessenger.Default;
        public const string Version = "1.0.0";

        public static T GetService<T>()
        {
            if (Instance.ServiceProvider.GetService<T>() is T service)
            {
                return service;
            }
            throw new ArgumentException($"{typeof(T)} 服务未找到");
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            var builder = new ServiceCollection()
                .AddSingleton<ILocalizeService, LocalizeService>()
                .AddSingleton<ISettingService, SettingService>()
                .AddSingleton<IScheduleService, ScheduleService>()
                .AddSingleton<INavigateService, NavigateService>()
                .AddTransient<IActivationService, ActivationService>()
                .AddSingleton<IWindowSizeService, WindowSizeService>()
                .AddTransient<MainViewModel>()
                .AddTransient<AllScheduleViewModel>()
                .AddTransient<SettingViewModel>()
                .AddTransient<AppInfoViewModel>()
                .AddTransient<MainWindowViewModel>();

            ServiceProvider = builder.BuildServiceProvider();

            GetService<IActivationService>().AppActivateAsync(null).Wait();
        }

        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            await GetService<IActivationService>().LaunchedActivateAsync(args);
        }
    }
}
