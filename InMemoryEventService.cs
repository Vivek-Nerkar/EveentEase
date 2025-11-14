// Services/InMemoryEventService.cs
using EventEase.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventEase.Services
{
    public class InMemoryEventService : IEventService
    {
        private readonly ConcurrentDictionary<Guid, Event> _events = new();

        public InMemoryEventService()
        {
            // seed data
            var seeds = new[]
            {
                new Event { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Quarterly Kickoff", Date = DateTime.Today.AddDays(7), Location = "Mumbai HQ" },
                new Event { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Design Workshop", Date = DateTime.Today.AddDays(14), Location = "Virtual" },
                new Event { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Client Gala", Date = DateTime.Today.AddDays(30), Location = "Hyderabad Convention Center" },
            };

            foreach (var s in seeds) _events[s.Id] = s;
        }

        public Task AddAsync(Event ev)
        {
            if (ev.Id == Guid.Empty) ev.Id = Guid.NewGuid();
            _events[ev.Id] = ev;
            return Task.CompletedTask;
        }

        public Task<int> CountAsync() => Task.FromResult(_events.Count);

        public Task<IReadOnlyList<Event>> GetAllAsync()
        {
            var list = _events.Values.OrderBy(e => e.Date).ToList();
            return Task.FromResult<IReadOnlyList<Event>>(list);
        }

        public Task<IReadOnlyList<Event>> GetPagedAsync(int skip, int take)
        {
            var page = _events.Values.OrderBy(e => e.Date).Skip(skip).Take(take).ToList();
            return Task.FromResult<IReadOnlyList<Event>>(page);
        }

        public Task<Event?> FindAsync(Guid id)
        {
            _events.TryGetValue(id, out var ev);
            return Task.FromResult(ev);
        }

        public Task UpdateAsync(Event ev)
        {
            if (ev == null) throw new ArgumentNullException(nameof(ev));
            if (!_events.ContainsKey(ev.Id)) throw new KeyNotFoundException("Event not found.");
            _events[ev.Id] = ev;
            return Task.CompletedTask;
        }
    }
}
