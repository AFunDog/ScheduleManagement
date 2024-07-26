using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CommunityToolkit.WinUI.Animations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using 日程管理系统.Core.Structs;
using 日程管理系统.ViewDatas;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统.Controls
{
    public sealed partial class SchedulePanel : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen",
            typeof(bool),
            typeof(SchedulePanel),
            new PropertyMetadata(true, OnIsOpenPropertyChanged)
        );

        public static readonly DependencyProperty SelectedScheduleProperty = DependencyProperty.Register(
            "SelectedSchedule",
            typeof(ScheduleData),
            typeof(SchedulePanel),
            new PropertyMetadata(null, OnSelectedSchedulePropertyChanged)
        );

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }
        public ScheduleData? SelectedSchedule
        {
            get => (ScheduleData?)GetValue(SelectedScheduleProperty);
            set => SetValue(SelectedScheduleProperty, value);
        }

        public string IconGlyph => (SelectedSchedule?.IsCompleted ?? false) ? "\uEA3B" : "\uEA3A";
        public TextDecorations TitleTextBlockTextDecoration =>
            (SelectedSchedule?.IsCompleted ?? false) ? TextDecorations.Strikethrough : TextDecorations.None;
        public Brush TextBlockForeground =>
            (SelectedSchedule?.IsCompleted ?? false)
                ? (App.Instance!.Resources["TextFillColorDisabledBrush"] as Brush)!
                : (App.Instance!.Resources["TextFillColorPrimaryBrush"] as Brush)!;
        public string StartTime =>
            (SelectedSchedule is null || !SelectedSchedule.StartTime.HasValue) ? "添加开始时间" : SelectedSchedule.StartTime.Value.ToString();
        public string EndTime =>
            (SelectedSchedule is null || !SelectedSchedule.EndTime.HasValue) ? "添加截止时间" : SelectedSchedule.EndTime.Value.ToString();

        public event PropertyChangedEventHandler? PropertyChanged;

        private double _openWidth;

        public SchedulePanel()
        {
            this.InitializeComponent();
            Loaded += (s, e) =>
            {
                _openWidth = Width;
                ToCloseState();
            };
        }

        private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SchedulePanel source = (d as SchedulePanel)!;
            if ((bool)e.NewValue)
            {
                source.ToOpenState();
            }
            else
            {
                source.ToCloseState();
            }
        }

        private static void OnSelectedSchedulePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SchedulePanel source = (d as SchedulePanel)!;
            source.UnregisterEvents(e.OldValue as ScheduleData);
            source.RegisterEvents(e.NewValue as ScheduleData);
        }

        private void RegisterEvents(ScheduleData? schedule)
        {
            if (schedule is null)
                return;
            schedule.PropertyChanged += OnSelectedScheduleInternalPropertyChanged;
            OnIsCompletedChanged();
        }

        private void UnregisterEvents(ScheduleData? schedule)
        {
            if (schedule is null)
                return;
            schedule.PropertyChanged -= OnSelectedScheduleInternalPropertyChanged;
        }

        private void OnSelectedScheduleInternalPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ScheduleData.IsCompleted):
                    OnIsCompletedChanged();
                    break;
                default:
                    break;
            }
        }

        private void OnIsCompletedChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconGlyph)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitleTextBlockTextDecoration)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextBlockForeground)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StartTime)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EndTime)));
        }

        private void ToCloseState()
        {
            AnimationBuilder
                .Create()
                .Size(Axis.X, to: 0, layer: FrameworkLayer.Xaml, duration: TimeSpan.FromMilliseconds(200))
                .Opacity(to: 0)
                .Start(this);
        }

        private void ToOpenState()
        {
            AnimationBuilder
                .Create()
                .Size(Axis.X, to: _openWidth, layer: FrameworkLayer.Xaml, duration: TimeSpan.FromMilliseconds(200))
                .Opacity(to: 1)
                .Start(this);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }
    }
}
