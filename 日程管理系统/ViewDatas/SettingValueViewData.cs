using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CoreServices.Setting.Structs;
using 日程管理系统.Core.Structs;
using 日程管理系统.ViewModels;

namespace 日程管理系统.ViewDatas
{
    public partial class SettingValueViewData : ObservableRecipient
    {
        private readonly SettingValue _settingValue;

        public SettingValueViewData(SettingValue settingValue)
        {
            _settingValue = settingValue;
            _value = settingValue.Value;
            _isChanged = false;
        }

        [ObservableProperty]
        private object _value;

        [ObservableProperty]
        private bool _isChanged;

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case nameof(Value):
                    IsChanged = !Value.Equals(_settingValue.Value);
                    Messenger.Send(new object(), SettingViewModel.SettingChanged);
                    break;
                default:
                    break;
            }
        }

        public virtual void ApplyChange()
        {
            _settingValue.Value = Value;
            IsChanged = !Value.Equals(_settingValue.Value);
            Messenger.Send(new object(), SettingViewModel.SettingChanged);
        }

        public virtual void CancelChange()
        {
            Value = _settingValue.Value;
        }
    }
}
