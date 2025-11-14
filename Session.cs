// Models/Session.cs
using System;

namespace EventEase.Models
{
    public class Session
    {
        public Guid SessionId { get; set; } = Guid.NewGuid();
        public string? AttendeeName { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional: add LastSeen, Preferences, AuthToken, etc.
        public DateTime LastSeen { get; set; } = DateTime.UtcNow;
    }
}
