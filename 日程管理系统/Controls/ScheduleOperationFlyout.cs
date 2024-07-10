using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using 日程管理系统.Contracts;
using 日程管理系统.ViewDatas;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统.Controls
{
    public sealed partial class ScheduleOperationFlyout : MenuFlyout
    {
        private sealed partial class ScheduleOperationFlyoutViewModel(FlyoutBase flyout) : ObservableObject
        {
            private readonly FlyoutBase _flyout = flyout;
            private static readonly IScheduleService _scheduleService = App.GetService<IScheduleService>();

            private ScheduleViewData TargetData => ((_flyout.Target as ScheduleCard)!.DataContext as ScheduleViewData)!;

            [RelayCommand]
            private void Delete()
            {
                _scheduleService.RemoveSchedule(TargetData);
            }
        }

        private ScheduleOperationFlyoutViewModel ViewModel { get; }

        public ScheduleOperationFlyout()
        {
            ViewModel = new(this);
            Items.Add(new MenuFlyoutItem() { Text = "删除", Icon = new SymbolIcon() { Symbol = Symbol.Delete }, Command = ViewModel.DeleteCommand });
        }


    }
}
