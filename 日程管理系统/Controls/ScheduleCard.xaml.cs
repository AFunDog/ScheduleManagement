using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CommunityToolkit.Mvvm.ComponentModel;
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
    public sealed partial class ScheduleCard : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ScheduleViewDataProperty = DependencyProperty.Register(
            "ScheduleData",
            typeof(ScheduleData),
            typeof(ScheduleCard),
            new PropertyMetadata(new ScheduleData(), OnScheduleDataPropertyChanged)
        );

        public ScheduleData ScheduleData
        {
            get => (ScheduleData)GetValue(ScheduleViewDataProperty);
            set => SetValue(ScheduleViewDataProperty, value);
        }

        public string IconGlyph => ScheduleData.IsCompleted ? "\uEA3B" : "\uEA3A";
        public TextDecorations TitleTextBlockTextDecoration =>
            ScheduleData.IsCompleted ? TextDecorations.Strikethrough : TextDecorations.None;
        public Brush TextBlockForeground =>
            ScheduleData.IsCompleted
                ? (App.Instance!.Resources["TextFillColorDisabledBrush"] as Brush)!
                : (App.Instance!.Resources["TextFillColorPrimaryBrush"] as Brush)!;

        public event Action<ScheduleCard>? ScheduleCardClick;

        public event PropertyChangedEventHandler? PropertyChanged;

        private static readonly FlyoutBase operationFlyout = new ScheduleOperationFlyout();

        public ScheduleCard()
        {
            this.InitializeComponent();
        }

        private void ScheduleCard_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                ScheduleCardClick?.Invoke(this);
            }
            else if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            {
                operationFlyout.ShowAt(this, new FlyoutShowOptions() { Position = e.GetCurrentPoint(this).Position });
            }
        }

        private static void OnScheduleDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScheduleCard source = (d as ScheduleCard)!;
            source.OnIsCompletedChanged();
        }

        private void CompletedIcon_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ScheduleData.IsCompleted = !ScheduleData.IsCompleted;
            if (ScheduleData.IsCompleted)
            {
                ToNormalState();
            }
            else
            {
                ToPointerOverState();
            }
            OnIsCompletedChanged();
            e.Handled = true;
        }

        private void OnIsCompletedChanged()
        {
            PropertyChanged?.Invoke(this, new(nameof(ScheduleData)));
            PropertyChanged?.Invoke(this, new(nameof(IconGlyph)));
            PropertyChanged?.Invoke(this, new(nameof(TitleTextBlockTextDecoration)));
            PropertyChanged?.Invoke(this, new(nameof(TextBlockForeground)));
        }

        private void ScheduleCard_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (ScheduleData.IsCompleted)
                return;

            ToPointerOverState();
            e.Handled = true;
        }

        private void ScheduleCard_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ToNormalState();
            e.Handled = true;
        }

        private void ToPointerOverState()
        {
            Root_Panel.Background = App.Instance.Resources["LayerFillColorAltBrush"] as SolidColorBrush;
        }

        private void ToNormalState()
        {
            Root_Panel.Background = App.Instance.Resources["LayerOnAccentAcrylicFillColorDefaultBrush"] as SolidColorBrush;
        }
    }
}
