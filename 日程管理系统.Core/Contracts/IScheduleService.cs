using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Core.Structs;

namespace 日程管理系统.Core.Contracts
{
    public interface IScheduleService
    {
        public ObservableCollection<ScheduleData> Schedules { get; }

        void AddSchedule(ScheduleData schedule);
        void RemoveSchedule(ScheduleData schedule);
        void ReadSchedules(string path);
        void SaveSchedules(string path);
    }
}
