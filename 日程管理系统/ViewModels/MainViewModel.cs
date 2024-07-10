using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoreServices.Localization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Contracts;
using 日程管理系统.Extensions;
using 日程管理系统.Structs;
using 日程管理系统.ViewDatas;

namespace 日程管理系统.ViewModels
{
    public partial class MainViewModel : ObservableRecipient
    {

        [ObservableProperty]
        private string _pageHeader = "MainPageHeaderUid".Localize();

        
    }
}
