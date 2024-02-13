using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsHotel.Code
{
    public class RoomRepository : IRoomRepository
    {
        public Room Get(Guid id)
        {
            Room entity = null;
            using (HotelContext context = new HotelContext())
            {
                entity = context.Room.FirstOrDefault(p => p.Id == id);
            }

            return entity;
        }

        public List<Room> Get(RoomFilter filter = null)
        {
            List<Room> toReturn = null;

            using (HotelContext context = new HotelContext())
            {
                var query = context.Room.AsQueryable();
                if (filter != null)
                {
                    if (filter.Id.HasValue)
                    {
                        query = query.Where(p => p.Id == filter.Id.Value);
                    }

                    if (!String.IsNullOrEmpty(filter.Text))
                    {
                        query = query.Where(p => p.Number.ToString().Contains(filter.Text));
                    }
                }

                toReturn = query.ToList();
            }

            return toReturn;
        }

        public Room Create(Room model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            bool result = false;
            Room toReturn = null;

            model.Id = Guid.NewGuid();
            model.DateCreated =
            model.DateUpdated = DateTime.Now;

            using (HotelContext context = new HotelContext())
            {
                context.Room.Add(model);
                result = context.SaveChanges() > 0;

                if (result)
                {
                    toReturn = Get(model.Id);
                }
            }

            return toReturn;
        }

        public bool Update(Room model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            Room entity = null;
            bool result = false;

            using (HotelContext context = new HotelContext())
            {
                entity = context.Room.Find(model.Id);

                if (entity != null)
                {
                    entity.HotelId = model.HotelId;
                    entity.OneBed = model.OneBed;
                    entity.Number = model.Number;
                    entity.DateUpdated = DateTime.Now;

                    result = context.SaveChanges() > 0;
                }
            }

            return result;
        }

        public bool Delete(Guid id)
        {
            bool result = false;

            using (HotelContext context = new HotelContext())
            {
                Room toDelete = context.Room.Find(id);

                if (toDelete != null)
                {
                    context.Room.Remove(toDelete);
                }

                result = context.SaveChanges() > 0;
            }

            return result;
        }
    }
}