using Vagabond.Web.Models;

namespace Vagabond.Web.Services
{
    public interface IDestinationService
    {
        Task<List<Destination>> GetAllAsync();
    }
}