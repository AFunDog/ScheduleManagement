using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 日程管理系统.Contracts
{
    public interface IActivationService
    {
        Task ActivateAsync(object? args);
    }
}
