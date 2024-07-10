using CommunityToolkit.Mvvm.ComponentModel;
using CoreServices.Localization;
using CoreServices.Setting.Structs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Extensions;
using 日程管理系统.Structs;

namespace 日程管理系统.ViewDatas
{
    public partial class SettingInfoViewData : ObservableObject
    {
        private readonly SettingInfo _settingInfo;

        public SettingInfoViewData(SettingInfo settingInfo)
        {
            _settingInfo = settingInfo;
            SettingName = $"Setting.{settingInfo.SettingKey}Uid".Localize();
            SettingDesc = $"Setting.{settingInfo.SettingKey}DescUid".Localize();
            IconGlyph = settingInfo.IconGlyph;
            if (settingInfo.SettingValue is SettingValueEnum)
            {
                _settingValue = new SettingValueEnumViewData(settingInfo.SettingKey, (SettingValueEnum)settingInfo.SettingValue);
            }
            else
            {
                _settingValue = new SettingValueViewData(settingInfo.SettingValue);
            }
        }

        [ObservableProperty]
        private string _settingName;
        [ObservableProperty]
        private string _settingDesc;
        [ObservableProperty]
        private string _iconGlyph;
        [ObservableProperty]
        private SettingValueViewData _settingValue;

        
    }
}
