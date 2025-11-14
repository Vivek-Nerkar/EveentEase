// Services/IAttendanceService.cs
using EventEase.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventEase.Services
{
    public interface IAttendanceService
    {
        Task AddAsync(AttendanceRecord record);
        Task<IReadOnlyList<AttendanceRecord>> GetByEventAsync(Guid eventId);
        Task<int> CountByEventAsync(Guid eventId);
        Task<IReadOnlyList<AttendanceRecord>> GetAllAsync();
    }
}
