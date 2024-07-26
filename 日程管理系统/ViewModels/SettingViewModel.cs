using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoreServices.Localization;
using CoreServices.Setting;
using CoreServices.Setting.Structs;
using 日程管理系统.Core.Structs;
using 日程管理系统.Extensions;
using 日程管理系统.Helpers;
using 日程管理系统.ViewDatas;

namespace 日程管理系统.ViewModels
{
    public partial class SettingViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private string _pageHeader = "SettingPageHeaderUid".Localize();

        [ObservableProperty]
        private string _affirmButtonText = "SettingPage.AffirmButtonTextUid".Localize();

        [ObservableProperty]
        private string _cancelButtonText = "SettingPage.CancelButtonTextUid".Localize();

        [ObservableProperty]
        private ObservableCollection<SettingInfoViewData> _settingInfos = [];

        public SettingViewModel(ISettingService settingService)
        {
            InitSettingInfos(settingService);
            Messenger.Register<SettingViewModel, object, string>(this, SettingChanged, OnSettingChanged);
        }

        private void InitSettingInfos(ISettingService settingService)
        {
            (string SettingKey, string IconGlyph, bool NeedReStart)[] ShowSettings =
            [
                new(SettingKeyLibrary.Theme, "\uE790", true),
                new(SettingKeyLibrary.WindowSize, "\uE744", false),
                new(SettingKeyLibrary.Language, "\uF2B7", true),
            ];
            //List<SettingInfoViewData> datas = [];
            foreach ((var key, var icon, var restart) in ShowSettings)
            {
                if (settingService.GetSetting(key) is SettingValue setting)
                {
                    SettingInfos.Add(new(new(key, icon, setting, restart)));
                }
            }
            //SettingInfos = datas;
        }

        public const string SettingChanged = nameof(SettingChanged);

        private void OnSettingChanged(SettingViewModel sender, object message)
        {
            AffirmChangeSettingsCommand.NotifyCanExecuteChanged();
            CancelChangeSettingsCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanAffirmChangeSettings))]
        private void AffirmChangeSettings()
        {
            foreach (var s in SettingInfos)
            {
                s.SettingValue.ApplyChange();
            }
            App.GetService<ISettingService>().SaveSettings(SettingKeyLibrary.SettingFile);
        }

        private bool CanAffirmChangeSettings()
        {
            return SettingInfos.Select(s => s.SettingValue.IsChanged).FirstOrDefault(b => b == true, false);
        }

        [RelayCommand(CanExecute = nameof(CanCancelChangeSettings))]
        private void CancelChangeSettings()
        {
            foreach (var s in SettingInfos)
            {
                s.SettingValue.CancelChange();
            }
            App.GetService<ISettingService>().SaveSettings(SettingKeyLibrary.SettingFile);
        }

        private bool CanCancelChangeSettings()
        {
            return SettingInfos.Select(s => s.SettingValue.IsChanged).FirstOrDefault(b => b == true, false);
        }
    }
}
