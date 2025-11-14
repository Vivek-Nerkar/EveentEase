// Services/AttendanceService.cs
using EventEase.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventEase.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ConcurrentBag<AttendanceRecord> _records = new();

        public Task AddAsync(AttendanceRecord record)
        {
            _records.Add(record);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<AttendanceRecord>> GetByEventAsync(Guid eventId)
        {
            var list = _records.Where(r => r.EventId == eventId).OrderByDescending(r => r.RegisteredAt).ToList();
            return Task.FromResult<IReadOnlyList<AttendanceRecord>>(list);
        }

        public Task<int> CountByEventAsync(Guid eventId)
        {
            var c = _records.Count(r => r.EventId == eventId);
            return Task.FromResult(c);
        }

        public Task<IReadOnlyList<AttendanceRecord>> GetAllAsync()
        {
            var list = _records.OrderByDescending(r => r.RegisteredAt).ToList();
            return Task.FromResult<IReadOnlyList<AttendanceRecord>>(list);
        }
    }
}
