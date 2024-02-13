using System;
using System.Collections.Generic;

namespace WindowsFormsHotel.Code
{
    public interface IReservationRepository
    {
        Reservation Create(Reservation model);
        bool Delete(Guid id);
        List<Reservation> Get(ReservationFilter filter);
        Reservation Get(Guid id);
        bool Update(Reservation model);
    }
}