using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;
using CoreServices.Localization;
using CoreServices.Setting;
using CoreServices.Setting.Structs;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using 日程管理系统.Contracts;
using 日程管理系统.Core.Contracts;
using 日程管理系统.Helpers;
using 日程管理系统.SettingValueCommands;
using 日程管理系统.Views;

namespace 日程管理系统.Services
{
    public class ActivationService : IActivationService
    {
        private readonly ILocalizeService _localizeService;
        private readonly ISettingService _settingService;
        private readonly IScheduleService _scheduleService;

        public ActivationService(ILocalizeService localizeService, ISettingService settingService, IScheduleService scheduleService)
        {
            _localizeService = localizeService;
            _settingService = settingService;
            _scheduleService = scheduleService;
        }

        public async Task AppActivateAsync(object? args)
        {
            List<Task> tasks = [InitLocalizeServiceAsync(), InitSettingServiceAsync(), InitScheduleServiceAsync()];

            await Task.WhenAll(tasks);
        }

        public async Task LaunchedActivateAsync(object? args)
        {
            var m_window = new MainWindow();
            App.GetService<IWindowSizeService>().TargetWindow = m_window;
            m_window.Activate();

            _settingService.TryExecuteAllCommands();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 加载本地化文件
        /// </summary>
        /// <returns></returns>
        private async Task InitLocalizeServiceAsync()
        {
            string LocDir = @"Localizations";
            if (!Directory.Exists(LocDir))
                Directory.CreateDirectory(LocDir);
            foreach (var locfile in Directory.GetFiles(LocDir, "*.json"))
            {
                try
                {
                    CultureInfo culture = new(Path.GetFileNameWithoutExtension(locfile));
                    var locs = JsonSerializer.Deserialize<Dictionary<string, string>>(File.OpenRead(locfile));
                    if (locs is null)
                        continue;
                    foreach ((var uid, var text) in locs)
                    {
                        _localizeService.SetLocalization(culture, uid, text);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    continue;
                }
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 初始化设置服务,并加载本地设置
        /// </summary>
        /// <returns></returns>
        private async Task InitSettingServiceAsync()
        {
            _settingService.BuildSettings(
                (builder) =>
                {
                    builder.Configure(
                        SettingKeyLibrary.Theme,
                        new SettingValueEnum(
                            0,
                            [
                                new("Default", ApplicationTheme.Dark),
                                new("Light", ApplicationTheme.Light),
                                new("Dark", ApplicationTheme.Dark),
                            ],
                            new SettingValueCommand((_, _) => { }, (_, _) => { })
                        )
                    );
                    builder.Configure(
                        SettingKeyLibrary.WindowSize,
                        new SettingValueEnum(
                            1,
                            [
                                new("Small", new PointInt32(960, 480)),
                                new("Medium", new PointInt32(1280, 720)),
                                new("Large", new PointInt32(1600, 900))
                            ],
                            new WindowSizeSettingValueCommand()
                        )
                    );
                    builder.Configure(
                        SettingKeyLibrary.Language,
                        new SettingValueEnum(
                            0,
                            [
                                new("System", CultureInfo.CurrentCulture),
                                new("zh-cn", new CultureInfo("zh-cn")),
                                new("en-us", new CultureInfo("en-us"))
                            ],
                            new SettingValueCommand(
                                (s, e) => { },
                                (s, e) =>
                                {
                                    App.GetService<ILocalizeService>().CurrentCultrue = (CultureInfo)e.NewValue;
                                }
                            )
                        )
                    );
                }
            );

            if (!File.Exists(SettingKeyLibrary.SettingFile))
            {
                _settingService.SaveSettings(SettingKeyLibrary.SettingFile);
            }
            else
            {
                _settingService.ReadSettings(SettingKeyLibrary.SettingFile);
            }
            // 设定应用主题
            if (_settingService.GetSetting(SettingKeyLibrary.Theme) is SettingValueEnum svm)
            {
                App.Instance.RequestedTheme = (ApplicationTheme)svm.Enum[(int)svm.Value].Parameter;
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 初始化日程服务
        /// </summary>
        /// <returns></returns>
        private async Task InitScheduleServiceAsync()
        {
            _scheduleService.ReadSchedules(ScheduleService.ScedulesFolder);

            await Task.CompletedTask;
        }
    }
}
