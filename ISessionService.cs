// Services/ISessionService.cs
using EventEase.Models;
using System.Threading.Tasks;

namespace EventEase.Services
{
    public interface ISessionService
    {
        Task<Session?> GetCurrentAsync();
        Task SaveAsync(Session session);
        Task ClearAsync();
        Task TouchAsync(); // update LastSeen
    }
}
