using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MessagePack;

namespace 日程管理系统.Core.Structs
{
    [MessagePackObject]
    public partial class ScheduleData : ObservableObject
    {
        [ObservableProperty]
        [property: Key(0)]
        private string _scheduleTitle = string.Empty;

        [ObservableProperty]
        [property: Key(1)]
        private string _scheduleDesc = string.Empty;

        [ObservableProperty]
        [property: Key(2)]
        private bool _isCompleted = false;

        [ObservableProperty]
        [property: Key(3)]
        private DateTime? _startTime = null;

        [ObservableProperty]
        [property: Key(4)]
        private DateTime? _endTime = null;
    }
}
