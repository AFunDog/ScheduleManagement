using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Contracts;
using 日程管理系统.Structs;

namespace 日程管理系统.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        private readonly IPageService _pageService;
        public MainWindowViewModel(IPageService pageService)
        {
            _pageService = pageService;

            _pages = _pageService.Pages;
        }

        [ObservableProperty]
        private IEnumerable<PageInfo> _pages;
    }
}
