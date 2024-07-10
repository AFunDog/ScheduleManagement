using CommunityToolkit.Mvvm.ComponentModel;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 日程管理系统.ViewDatas
{
    public partial class ScheduleViewData : ObservableObject
    {

        [ObservableProperty]
        private string _scheduleTitle = string.Empty;

        [ObservableProperty]
        private string _scheduleDesc  = string.Empty;

        [ObservableProperty]
        private bool _isCompleted = false;

    }
}
