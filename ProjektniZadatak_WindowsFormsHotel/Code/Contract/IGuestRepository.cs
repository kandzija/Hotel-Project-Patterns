using System.Collections.Generic;
using System;

namespace WindowsFormsHotel.Code
{
    public interface IGuestRepository
    {
        Guest Create(Guest model);
        bool Delete(Guid id);
        List<Guest> Get(GuestFilter filter = null);
        Guest Get(Guid id);
        bool Update(Guest model);


    }
}