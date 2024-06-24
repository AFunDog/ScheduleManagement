using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using WinRT;
using 日程管理系统.Structs;

namespace 日程管理系统.Behaviors
{
    public class NavigationViewBehavior : Behavior<NavigationView>
    {
        public static readonly DependencyProperty FrameProperty = DependencyProperty.Register(
            "Frame",
            typeof(Frame),
            typeof(NavigationViewBehavior),
            new(null)
        );

        public Frame? Frame
        {
            get => (Frame)GetValue(FrameProperty);
            set
            {
                if (Frame is not null)
                    Frame.Navigated -= Frame_Navigated;
                SetValue(FrameProperty, value);
                if (value is not null)
                    value.Navigated += Frame_Navigated;
            }
        }

        private void Frame_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {

        }

        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += NavigationView_SelectionChanged;
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            Frame?.Navigate(((PageInfo)args.SelectedItem).PageType, null);
            //Debug.WriteLine($"SelectionChanged : {args.SelectedItemContainer} {args.SelectedItem}");
            //Debug.WriteLine(AssociatedObject.ContainerFromMenuItem(args.SelectedItem) == args.SelectedItemContainer);
        }

        protected override void OnDetaching() { }
    }
}
