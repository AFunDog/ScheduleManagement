using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mapster;
using MessagePack;
using 日程管理系统.Contracts;
using 日程管理系统.Structs;
using 日程管理系统.ViewDatas;

namespace 日程管理系统.Services
{
    public class ScheduleService : IScheduleService
    {
        public const string ScedulesFolder = "Schedules";
        public const string SchedulesFileExtension = ".sch";

        private readonly ObservableCollection<ScheduleViewData> _schedules = [];
        public ObservableCollection<ScheduleViewData> Schedules => _schedules;

        public void AddSchedule(ScheduleViewData schedule)
        {
            _schedules.Add(schedule);
        }

        public void RemoveSchedule(ScheduleViewData schedule)
        {
            _schedules.Remove(schedule);
        }

        public void ReadSchedules(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return;
            }
            foreach (var file in Directory.GetFiles(path, $"*{SchedulesFileExtension}"))
            {
                var ss = MessagePackSerializer.Deserialize<IEnumerable<ScheduleInfo>>(File.ReadAllBytes(file));
                foreach (var s in ss)
                {
                    ScheduleViewData scheduleViewData = new();
                    s.Adapt(scheduleViewData);
                    _schedules.Add(scheduleViewData);
                }
            }
        }

        public void SaveSchedules(string path)
        {
            if (!File.Exists(path))
                File.Create(path).Close();
            var ss = _schedules.Select(svd =>
            {
                ScheduleInfo si = new();
                svd.Adapt(si);
                return si;
            });
            File.WriteAllBytes(path, MessagePackSerializer.Serialize(ss));
        }
    }
}
