using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统.Controls
{
    public sealed partial class TimePickerButton : Button, INotifyPropertyChanged
    {
        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
            "PlaceholderText",
            typeof(string),
            typeof(TimePickerButton),
            new PropertyMetadata(string.Empty)
        );
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time",
            typeof(DateTime?),
            typeof(TimePickerButton),
            new PropertyMetadata(null)
        );

        public DateTime? Time
        {
            get
            {
                return (DateTime?)GetValue(TimeProperty);
            }
            set
            {
                SelectedDate = value.HasValue ? value.Value.Date : null;
                SetValue(TimeProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowText)));
            }
        }
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set
            {
                SetValue(PlaceholderTextProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowText)));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private string ShowText => !Time.HasValue ? PlaceholderText : Time.Value.ToString();
        private DateTime? SelectedDate { get; set; } = null;
        private DateTime? SelectedTime
        {
            get
            {
                if (SelectedDate.HasValue)
                {
                    return SelectedDate.Value + (Flyout_TimePicker.SelectedTime ?? default);
                }
                else
                {
                    return null;
                }
            }
        }
        public TimePickerButton()
        {
            this.InitializeComponent();
        }


        private void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            SelectedDate = (args.AddedDates.Count == 0 ? null : args.AddedDates[0].Date);
            RaiseTimePropertyChanged();
        }

        private void TimePicker_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            
            RaiseTimePropertyChanged();
        }

        private void RaiseTimePropertyChanged()
        {
            SetValue(TimeProperty, SelectedTime);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowText)));
        }

        private void TimePickerButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            {
                SelectedDate = null;
                Flyout_TimePicker.SelectedTime = null;
                RaiseTimePropertyChanged();
            }
        }
    }
}
