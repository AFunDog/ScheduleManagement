using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CoreServices.Localization;
using CoreServices.WinUI.Common;
using CoreServices.WinUI.Contracts;
using CoreServices.WinUI.Structs;
using Mapster;
using Microsoft.UI.Xaml.Media.Animation;
using 日程管理系统.Contracts;
using 日程管理系统.Extensions;
using 日程管理系统.Structs;
using 日程管理系统.Views;

namespace 日程管理系统.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private string _appSubTitle;

        [ObservableProperty]
        private ObservableCollection<IPageItem> _headerPageItems =
        [
            new PageItem(
                "MainPageTitleUid".Localize(),
                "\uE74C",
                typeof(MainPage),
                new NavigateTransitionSelector(t =>
                {
                    if (t == typeof(AllSchedulePage))
                        return new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight };
                    return null;
                })
            ),
            new SeparatorItem(),
            new PageItem(
                "AllSchedulePageTitleUid".Localize(),
                "\uE71D",
                typeof(AllSchedulePage),
                new NavigateTransitionSelector(t =>
                {
                    if (t == typeof(MainPage))
                        return new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft };
                    return null;
                })
            ),
        ];

        [ObservableProperty]
        private ObservableCollection<IPageItem> _footerPageItems =
        [
            new SeparatorItem(),
            new PageItem("AppInfoPageTitleUid".Localize(), "\uE946", typeof(AppInfoPage)),
            new PageItem("SettingPageTitleUid".Localize(), "\uE713", typeof(SettingPage))
        ];

        public MainWindowViewModel()
        {
            AppSubTitle = "AppSubTitleUid".Localize();
        }
    }
}
