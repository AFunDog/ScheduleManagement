using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Contracts;
using 日程管理系统.Controls;
using 日程管理系统.Extensions;
using 日程管理系统.ViewDatas;

namespace 日程管理系统.ViewModels
{
    public partial class AllScheduleViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private string _pageHeader = "AllSchedulePageTitleUid".Localize();

        [ObservableProperty]
        private ObservableCollection<ScheduleViewData> _schedules = App.GetService<IScheduleService>().Schedules;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddScheduleCommand))]
        private string _addScheduleTitle = string.Empty;

        [ObservableProperty]
        private bool _isSchedulePanelOpen = false;

        [ObservableProperty]
        private ScheduleViewData? _selectedScheduleViewData;

        public void OnScheduleCardClicked(ScheduleCard scheduleCard)
        {
            IsSchedulePanelOpen = true;
            SelectedScheduleViewData = scheduleCard.DataContext as ScheduleViewData;
        }

        [RelayCommand(CanExecute = nameof(CanAddSchedule))]
        private void AddSchedule()
        {
            App.GetService<IScheduleService>().AddSchedule(new() { ScheduleTitle = AddScheduleTitle });
        }
        private bool CanAddSchedule() => !string.IsNullOrEmpty(AddScheduleTitle);
    }
}
