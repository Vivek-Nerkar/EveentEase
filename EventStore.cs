// Shared/EventStore.cs
using EventEase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventEase
{
    public static class EventStore
    {
        private static List<Event> _events = new List<Event>
        {
            new Event { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Quarterly Kickoff", Date = DateTime.Today.AddDays(7), Location = "Mumbai HQ" },
            new Event { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Design Workshop", Date = DateTime.Today.AddDays(14), Location = "Virtual" },
            new Event { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Client Gala", Date = DateTime.Today.AddDays(30), Location = "Hyderabad Convention Center" },
        };

        public static IReadOnlyList<Event> All() => _events;
        public static Event? Find(Guid id) => _events.FirstOrDefault(e => e.Id == id);
        public static void Update(Event ev)
        {
            var idx = _events.FindIndex(x => x.Id == ev.Id);
            if (idx >= 0) _events[idx] = ev;
        }
    }
}
