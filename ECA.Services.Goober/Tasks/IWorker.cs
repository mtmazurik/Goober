using System.Threading;
using System.Threading.Tasks;

namespace ECA.Services.Goober.Tasks
{
    public interface IWorker
    {
        Task DoTheTask();
    }
}