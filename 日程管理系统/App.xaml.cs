using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using CoreServices.Localization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using 日程管理系统.Contracts;
using 日程管理系统.Services;
using 日程管理系统.ViewModels;
using 日程管理系统.Views;

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
                .AddSingleton<LocalizeService>()
                .AddSingleton<IPageService, PageService>()
                .AddTransient<IActivationService,ActivationService>()
                .AddTransient<MainWindowViewModel>();


            ServiceProvider = builder.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider
        {
            get; private set;
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            await GetService<IActivationService>().ActivateAsync(args.Arguments);


            var m_window = new MainWindow();
            m_window.Activate();
        }

    }
}
