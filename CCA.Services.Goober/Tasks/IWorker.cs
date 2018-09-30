using System.Threading;
using System.Threading.Tasks;

namespace CCA.Services.Goober.Tasks
{
    public interface IWorker
    {
        Task DoTheTask();
    }
}