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
using 日程管理系统.Core.Contracts;
using 日程管理系统.Core.Structs;

namespace 日程管理系统.Services
{
    public class ScheduleService : IScheduleService
    {
        public const string ScedulesFolder = "Schedules";
        public const string SchedulesFileExtension = ".sch";

        private readonly ObservableCollection<ScheduleData> _schedules = [];
        public ObservableCollection<ScheduleData> Schedules => _schedules;

        public void AddSchedule(ScheduleData schedule)
        {
            _schedules.Add(schedule);
        }

        public void RemoveSchedule(ScheduleData schedule)
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
                var ss = MessagePackSerializer.Deserialize<IEnumerable<ScheduleData>>(File.ReadAllBytes(file));
                foreach (var s in ss)
                {
                    _schedules.Add(s);
                }
            }
        }

        public void SaveSchedules(string path)
        {
            if (!File.Exists(path))
                File.Create(path).Close();
            File.WriteAllBytes(path, MessagePackSerializer.Serialize(_schedules));
        }
    }
}
