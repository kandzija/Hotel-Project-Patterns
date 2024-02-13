using System;

namespace WindowsFormsHotel.Code
{
    public class ReservationFilter : BaseFilter
    {
        public Guid? ReservationId { get; set; }

        public Guid? RoomId { get; set; }

        public Guid? GuestId { get; set; }
    }
}