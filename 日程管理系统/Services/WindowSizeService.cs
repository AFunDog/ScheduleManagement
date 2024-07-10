using CommunityToolkit.WinUI.Animations;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Spi;
using WinUIEx;
using 日程管理系统.Contracts;

namespace 日程管理系统.Services
{
    public class WindowSizeService : IWindowSizeService
    {
        public Window? TargetWindow { get; set; }

        public void Resize(int width, int height)
        {
            if (TargetWindow is null)
                return;

            TargetWindow.SetWindowSize(width, height);
        }
    }
}
