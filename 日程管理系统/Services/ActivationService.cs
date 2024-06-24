using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 日程管理系统.Contracts;
using 日程管理系统.Views;

namespace 日程管理系统.Services
{
    public class ActivationService : IActivationService
    {
        private readonly IPageService _pageService;

        public ActivationService(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task ActivateAsync(object? args)
        {
            List<Task> tasks = [InitPageServiceAsync()];
            await Task.WhenAll(tasks);
        }

        private async Task InitPageServiceAsync()
        {
            _pageService.Configure<MainPage>("MainPageTitleUid", "\uE74C");
            await Task.CompletedTask;
        }
    }
}
