using CoreServices.Setting.Structs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.ViewDatas;

namespace 日程管理系统.TemplateSelectors
{
    public class SettingCardOperatingControlTemplateSelector : DataTemplateSelector
    {
        private readonly static DataTemplate DefaultTemplate = new();
        public DataTemplate EnumTemplate { get; set; } = new();
        public DataTemplate NumberTemplate { get; set; } = new();
        public DataTemplate StringTemplate { get; set; } = new();

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            if (item is SettingValueViewData svd)
            {
                if (svd is SettingValueEnumViewData)
                {
                    return EnumTemplate;
                }
                else
                {
                    switch (svd.Value)
                    {
                        case string:
                            return StringTemplate;
                        case int:
                            return NumberTemplate;
                        default:
                            break;
                    }
                }
            }
            return DefaultTemplate;
        }
    }
}
