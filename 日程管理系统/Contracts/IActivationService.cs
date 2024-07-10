using System.Threading.Tasks;

namespace 日程管理系统.Contracts
{
    public interface IActivationService
    {
        Task AppActivateAsync(object? args);
        Task LaunchedActivateAsync(object? args);
    }
}