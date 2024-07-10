using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.Animations;
using CommunityToolkit.WinUI.Animations.Expressions;
using CoreServices.Localization;
using CoreServices.WinUI.Contracts;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using WinUIEx;
using 日程管理系统.Contracts;
using 日程管理系统.Extensions;
using 日程管理系统.ViewModels;
using 日程管理系统.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx
    {
        public MainWindowViewModel ViewModel { get; set; }

        public MainWindow()
        {
            this.SetWindowSize(1280, 720);
            ViewModel = App.GetService<MainWindowViewModel>();
            this.InitializeComponent();

            Title = "AppTitleUid".Localize();


            NavigationViewControl.Loaded += (s, e) =>
            {
                App.GetService<INavigateService>().AttachService(NavigationViewControl.ContentFrame);
                App.GetService<INavigateService>().Navigate(typeof(MainPage));

            };
        }
    }
}