// Models/Event.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(200, ErrorMessage = "Name must be at most 200 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, ErrorMessage = "Location must be at most 200 characters.")]
        public string Location { get; set; } = string.Empty;

        // Optional: a sanitized short description
        [StringLength(1000, ErrorMessage = "Description too long.")]
        public string? Description { get; set; }
    }
}
