using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using CommunityToolkit.WinUI.Animations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Devices.Enumeration;
using 日程管理系统.ViewDatas;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统.Controls
{
    public sealed class SchedulePanel : ContentControl
    {
        public static DependencyProperty IsOpenProperty { get; } =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(SchedulePanel), new PropertyMetadata(true, OnIsOpenPropertyChanged));
        public static DependencyProperty SelectedScheduleProperty { get; } =
            DependencyProperty.Register(
                "SelectedSchedule",
                typeof(ScheduleViewData),
                typeof(SchedulePanel),
                new PropertyMetadata(new ScheduleViewData())
            );
        public static DependencyProperty ScheduleTitleProperty { get; } =
            DependencyProperty.Register("ScheduleTitle", typeof(string), typeof(SchedulePanel), new PropertyMetadata(string.Empty));
        public static DependencyProperty ScheduleDescProperty { get; } =
            DependencyProperty.Register("ScheduleDesc", typeof(string), typeof(SchedulePanel), new PropertyMetadata(string.Empty));
        public static DependencyProperty IsCompletedProperty { get; } =
            DependencyProperty.Register("IsCompleted", typeof(bool), typeof(SchedulePanel), new PropertyMetadata(false));
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }
        public ScheduleViewData SelectedSchedule
        {
            get => (ScheduleViewData)GetValue(SelectedScheduleProperty);
            set
            {
                SetValue(SelectedScheduleProperty, value);
                SetBinding(
                    ScheduleTitleProperty,
                    new Binding()
                    {
                        Path = new PropertyPath("ScheduleTitle"),
                        Source = value,
                        Mode = BindingMode.TwoWay
                    }
                );
                SetBinding(
                    ScheduleDescProperty,
                    new Binding()
                    {
                        Path = new PropertyPath("ScheduleDesc"),
                        Source = value,
                        Mode = BindingMode.TwoWay
                    }
                );
                SetBinding(
                    IsCompletedProperty,
                    new Binding()
                    {
                        Path = new PropertyPath("IsCompleted"),
                        Source = value,
                        Mode = BindingMode.TwoWay
                    }
                );
            }
        }
        public string ScheduleTitle
        {
            get => (string)GetValue(ScheduleTitleProperty);
            set => SetValue(ScheduleTitleProperty, value);
        }
        public string ScheduleDesc
        {
            get => (string)GetValue(ScheduleDescProperty);
            set => SetValue(ScheduleDescProperty, value);
        }
        public bool IsCompleted
        {
            get => (bool)GetValue(IsCompletedProperty);
            set => SetValue(IsCompletedProperty, value);
        }

        private double _width;

        public SchedulePanel()
        {
            this.DefaultStyleKey = typeof(SchedulePanel);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            RegisterEvents();
            _width = Width;
            ToCloseState();
        }

        private void RegisterEvents()
        {
            if (GetTemplateChild("CloseButton") is Button closeButton)
            {
                closeButton.Click += CloseButton_Click;
            }
            if(GetTemplateChild("DescTextBox") is TextBox descTextBox)
            {
                descTextBox.TextChanged += DescTextBox_TextChanged;
            }
        }

        private void DescTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScheduleDesc = (sender as TextBox)!.Text;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }

        private static void OnIsOpenPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is SchedulePanel panel)
            {
                if (args.NewValue is true)
                {
                    panel.ToOpenState();
                }
                else
                {
                    panel.ToCloseState();
                }
            }
        }

        private async void ToOpenState()
        {
            await AnimationBuilder
                .Create()
                .Size(Axis.X, to: _width, layer: FrameworkLayer.Xaml, duration: TimeSpan.FromMilliseconds(200))
                .Opacity(to: 1)
                .StartAsync(this);
        }

        private async void ToCloseState()
        {
            await AnimationBuilder
                .Create()
                .Size(Axis.X, to: 0, layer: FrameworkLayer.Xaml, duration: TimeSpan.FromMilliseconds(200))
                .Opacity(to: 0)
                .StartAsync(this);
        }
    }
}
