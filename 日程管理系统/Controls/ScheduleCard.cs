using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统.Controls
{
    public sealed class ScheduleCard : ButtonBase
    {
        public static readonly DependencyProperty ScheduleTitleProperty = DependencyProperty.Register(
            "ScheduleTitle",
            typeof(string),
            typeof(ScheduleCard),
            new PropertyMetadata("日程标题")
        );
        public static readonly DependencyProperty ScheduleDescProperty = DependencyProperty.Register(
            "ScheduleDesc",
            typeof(string),
            typeof(ScheduleCard),
            new PropertyMetadata("日程描述")
        );
        public static readonly DependencyProperty IsCompletedProperty = DependencyProperty.Register(
            "IsCompleted",
            typeof(bool),
            typeof(ScheduleCard),
            new PropertyMetadata(false)
        );
        public static readonly DependencyProperty IconGlyphProperty = DependencyProperty.Register(
            "IconGlyph",
            typeof(string),
            typeof(ScheduleCard),
            new PropertyMetadata("\uEA3A")
        );

        public string ScheduleTitle
        {
            get { return (string)GetValue(ScheduleTitleProperty); }
            set { SetValue(ScheduleTitleProperty, value); }
        }
        public string ScheduleDesc
        {
            get { return (string)GetValue(ScheduleDescProperty); }
            set { SetValue(ScheduleDescProperty, value); }
        }
        public bool IsCompleted
        {
            get { return (bool)GetValue(IsCompletedProperty); }
            set { SetValue(IsCompletedProperty, value); SetValue(IconGlyphProperty, value ? "\uEA3B" : "\uEA3A"); }
        }
        public event Action<ScheduleCard>? ScheduleCardClicked;
        public string IconGlyph => (string)GetValue(IconGlyphProperty);

        private FrameworkElement? _completedIcon;
        private static readonly FlyoutBase _operationFlyout = new ScheduleOperationFlyout();

        public ScheduleCard()
        {
            this.DefaultStyleKey = typeof(ScheduleCard);

        }




        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _completedIcon = GetTemplateChild("CompletedIcon") as FrameworkElement;
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            if (_completedIcon is not null)
            {
                _completedIcon.PointerPressed += CompletedIcon_PointerPressed;
            }
        }

        private void CompletedIcon_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var properties = e.GetCurrentPoint(_completedIcon).Properties;
            if (properties.IsLeftButtonPressed)
            {
                IsCompleted = !IsCompleted;
                if (IsCompleted)
                {
                    VisualStateManager.GoToState(this, "Completed", true);
                }
                else
                {
                    VisualStateManager.GoToState(this, "PointerOver", true);
                }
                e.Handled = true;
            }
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);
            var properties = e.GetCurrentPoint(this).Properties;
            if (properties.IsLeftButtonPressed)
            {
                ScheduleCardClicked?.Invoke(this);
                e.Handled = true;
            }
            else if (properties.IsRightButtonPressed)
            {
                _operationFlyout?.ShowAt(this, new FlyoutShowOptions() { Position = e.GetCurrentPoint(this).Position });
                e.Handled = true;
            }


        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);
            if (IsCompleted)
                return;
            VisualStateManager.GoToState(this, "PointerOver", true);
        }


        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);
            if (IsCompleted)
                return;
            VisualStateManager.GoToState(this, "Normal", true);
        }
    }
}
