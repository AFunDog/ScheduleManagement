using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Contracts;
using 日程管理系统.Structs;

namespace 日程管理系统.Services
{
    public sealed class PageService : IPageService
    {
        private readonly List<PageInfo> _pages = [];
        public IEnumerable<PageInfo> Pages => _pages;

        public IPageService Configure<PageType>(string PageNameUid, string IconGlyph) where PageType : Page
        {
            if (_pages.Find(p => p.PageType == typeof(PageType)) == default)
            {
                _pages.Add(new(PageNameUid,IconGlyph,typeof(PageType)));
            }
            return this;
        }
    }
}
