using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
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

namespace 日程管理系统.UserControls
{
    public sealed partial class CustomTitleBar : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(string),
            typeof(CustomTitleBar),
            new PropertyMetadata("标题")
        );
        public static readonly DependencyProperty SubTitleProperty = DependencyProperty.Register(
            "SubTitle",
            typeof(string),
            typeof(CustomTitleBar),
            new PropertyMetadata("副标题")
        );
        public static readonly DependencyProperty TargetWindowProperty =
            DependencyProperty.Register(
                "TargetWindow",
                typeof(Window),
                typeof(CustomTitleBar),
                new PropertyMetadata(null)
            );

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public string SubTitle
        {
            get { return (string)GetValue(SubTitleProperty); }
            set { SetValue(SubTitleProperty, value); }
        }
        public Window? TargetWindow
        {
            get { return GetValue(TargetWindowProperty) as Window; }
            set { SetValue(TargetWindowProperty, value); }
        }

        public CustomTitleBar()
        {
            this.InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            TargetWindow?.DispatcherQueue.TryEnqueue(TargetWindow.Close);
        }

        private bool _isDragging = false;
        private System.Drawing.Point _pointerPos;
        private global::Windows.Graphics.PointInt32 _windowPos;

        private void OnPointerPressed(
            object sender,
            Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e
        )
        {
            if (
                sender is UIElement element
                && e.GetCurrentPoint(element).Properties.IsLeftButtonPressed
            )
            {
                _isDragging = true;
                _windowPos = TargetWindow?.AppWindow.Position ?? default;
                _pointerPos = GetCursorPos(out var point) ? point : default;
                CapturePointer(e.Pointer);
                e.Handled = true;
            }
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ReleasePointerCapture(e.Pointer);
            _isDragging = false;
            e.Handled = true;
        }

        private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (_isDragging)
            {
                GetCursorPos(out System.Drawing.Point pointerPos);
                TargetWindow?.AppWindow.Move(
                    new(
                        _windowPos.X + pointerPos.X - _pointerPos.X,
                        _windowPos.Y + pointerPos.Y - _pointerPos.Y
                    )
                );
            }
        }

        /// <summary>
        /// 获取鼠标相对于屏幕的位置
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool GetCursorPos(out System.Drawing.Point lpPoint);
    }
}
