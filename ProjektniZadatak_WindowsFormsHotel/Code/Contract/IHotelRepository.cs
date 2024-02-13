using System;

namespace WindowsFormsHotel.Code
{
    public interface IHotelRepository
    {
        Hotel Get();
        bool Update(Hotel model);
        Hotel Create(Hotel model);
    
    }
}