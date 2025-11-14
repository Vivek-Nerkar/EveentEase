// Services/IEventService.cs
using EventEase.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventEase.Services
{
    public interface IEventService
    {
        Task<IReadOnlyList<Event>> GetPagedAsync(int skip, int take);
        Task<int> CountAsync();
        Task<Event?> FindAsync(Guid id);
        Task UpdateAsync(Event ev);
        Task AddAsync(Event ev);
        Task<IReadOnlyList<Event>> GetAllAsync(); // for small/dev scenarios
    }
}
