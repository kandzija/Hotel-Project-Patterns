using System;
using System.Collections.Generic;

namespace WindowsFormsHotel.Code
{
    public interface IRoomRepository
    {
        Room Create(Room model);
        bool Delete(Guid id);
        List<Room> Get(RoomFilter filter = null);
        Room Get(Guid id);
        bool Update(Room model);
    }
}