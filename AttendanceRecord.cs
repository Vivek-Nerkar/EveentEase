// Models/AttendanceRecord.cs
using System;

namespace EventEase.Models
{
    public class AttendanceRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EventId { get; set; }
        public string AttendeeName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public bool Confirmed { get; set; } = false; // future: confirm via email or check-in
    }
}
