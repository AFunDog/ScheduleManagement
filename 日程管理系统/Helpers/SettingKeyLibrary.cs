using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace 日程管理系统.Helpers
{
    public static class SettingKeyLibrary
    {
        public const string SettingFile = ".appsettings";

        public const string Theme = nameof(Theme);
        public const string WindowSize = nameof(WindowSize);
        public const string Language = nameof(Language);
    }
}
