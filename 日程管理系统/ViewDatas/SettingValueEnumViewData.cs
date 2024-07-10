using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CoreServices.Localization;
using CoreServices.Setting.Structs;
using 日程管理系统.Extensions;
using 日程管理系统.Structs;

namespace 日程管理系统.ViewDatas
{
    public partial class SettingValueEnumViewData : SettingValueViewData
    {
        public SettingValueEnumViewData(string key, SettingValueEnum settingValue)
            : base(settingValue)
        {
            var localizeService = App.GetService<ILocalizeService>();
            _enumOptions = new(settingValue.Enum.Select(x => $"Setting.{key}.{x.EnumUid}Uid".Localize()));
        }

        public int ValueToIndex(object value) => (int)value;

        public object IndexToValue(int index)
        {
            if(index != -1)
            {
                Value = index;
            }
            return index;
        }

        [ObservableProperty]
        private ObservableCollection<string> _enumOptions = [];

        [ObservableProperty]
        private ObservableCollection<object> _enumParameters = [];
    }
}
