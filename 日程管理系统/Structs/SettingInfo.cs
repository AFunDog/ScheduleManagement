using CoreServices.Setting.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 日程管理系统.Structs
{

    public record SettingInfo(string SettingKey, string IconGlyph,SettingValue SettingValue,bool NeedReStart)
    {
    }
}
