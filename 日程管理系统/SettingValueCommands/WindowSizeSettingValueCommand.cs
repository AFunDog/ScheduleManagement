using CoreServices.Setting.Contracts;
using CoreServices.Setting.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics;
using 日程管理系统.Contracts;

namespace 日程管理系统.SettingValueCommands
{
    public sealed class WindowSizeSettingValueCommand : ISettingValueCommand
    {
        public bool CanExecuteChangedCommand(SettingValue sender, SettingValueChangeEvenArgs args)
        {
            return true;
        }

        public bool CanExecuteChangingCommand(SettingValue sender, SettingValueChangeEvenArgs args)
        {
            return true;
        }

        public bool CanModifySettingValue(SettingValue sender, SettingValueChangeEvenArgs args)
        {
            return true;
        }

        public void ExecuteSettingValueChangedCommand(SettingValue sender, SettingValueChangeEvenArgs args)
        {
            PointInt32 size = (PointInt32)args.NewValue;
            App.GetService<IWindowSizeService>().Resize(size.X, size.Y);
        }

        public void ExecuteSettingValueChangingCommand(SettingValue sender, SettingValueChangeEvenArgs args)
        {

        }
    }
}
