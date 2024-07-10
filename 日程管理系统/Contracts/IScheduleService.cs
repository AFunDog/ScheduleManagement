using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Structs;
using 日程管理系统.ViewDatas;

namespace 日程管理系统.Contracts
{
    public interface IScheduleService
    {
        public ObservableCollection<ScheduleViewData> Schedules { get; }

        void AddSchedule(ScheduleViewData schedule);
        void RemoveSchedule(ScheduleViewData schedule);
        void ReadSchedules(string path);
        void SaveSchedules(string path);
    }
}
