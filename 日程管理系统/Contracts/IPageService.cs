using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using 日程管理系统.Structs;

namespace 日程管理系统.Contracts
{
    public interface IPageService
    {
        IEnumerable<PageInfo> Pages { get; }

        IPageService Configure<PageType>(string PageNameUid, string IconGlyph)
            where PageType : Page;
    }
}
