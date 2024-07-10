using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using 日程管理系统.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace 日程管理系统.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            ViewModel = App.GetService<MainViewModel>();
            this.InitializeComponent();
        }
    }
}