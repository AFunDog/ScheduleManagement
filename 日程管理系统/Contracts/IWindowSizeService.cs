using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace 日程管理系统.Contracts
{
    public interface IWindowSizeService
    {
        public Window? TargetWindow { get; set; }
        void Resize(int width, int height);
    }
}
