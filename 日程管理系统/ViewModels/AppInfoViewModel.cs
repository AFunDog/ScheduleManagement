using CommunityToolkit.Mvvm.ComponentModel;
using CoreServices.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Extensions;

namespace 日程管理系统.ViewModels
{
    public partial class AppInfoViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private string _pageHeader = "AppInfoPageHeaderUid".Localize();

        [ObservableProperty]
        private string _appInfoHeader = "AppInfoHeaderUid".Localize();

        [ObservableProperty]
        private string _appVersionHeader = "AppVersionHeaderUid".Localize();

        [ObservableProperty]
        private string _appVersion = App.Version;
    }
}
