using System;
using System.Collections.Generic;

#nullable disable

namespace BookingSystem.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int BookedQuantity { get; set; }
        public int ResourceId { get; set; }

        public virtual Resource Resource { get; set; }
    }
}
