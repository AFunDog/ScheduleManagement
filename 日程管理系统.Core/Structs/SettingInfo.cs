using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreServices.Setting.Structs;

namespace 日程管理系统.Core.Structs
{
    public record SettingInfo(string SettingKey, string IconGlyph, SettingValue SettingValue, bool NeedReStart) { }
}
