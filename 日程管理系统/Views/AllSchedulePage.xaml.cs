using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using 日程管理系统.ViewModels;
using 日程管理系统.Contracts;
using 日程管理系统.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllSchedulePage : Page
    {
        public AllScheduleViewModel ViewModel { get; set; }

        public AllSchedulePage()
        {
            ViewModel = App.GetService<AllScheduleViewModel>();
            this.InitializeComponent();
            this.Unloaded += AllSchedulePage_Unloaded;
        }

        private void AllSchedulePage_Unloaded(object sender, RoutedEventArgs e)
        {
            App.GetService<IScheduleService>().SaveSchedules(@$"{ScheduleService.ScedulesFolder}\schs{ScheduleService.SchedulesFileExtension}");
        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // 添加日程
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                ViewModel.AddScheduleCommand.Execute(null);
                (sender as TextBox)!.Text = string.Empty;
            }
        }
    }
}
