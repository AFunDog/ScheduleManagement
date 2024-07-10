using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace 日程管理系统.Structs
{
    [MessagePackObject]
    public class ScheduleInfo
    {
        [Key(0)]
        public string ScheduleTitle { get; set; } = string.Empty;
        [Key(1)]
        public string ScheduleDesc { get; set; } = string.Empty;
        [Key(2)]
        public bool IsCompleted { get; set; } = false;
    }
}
