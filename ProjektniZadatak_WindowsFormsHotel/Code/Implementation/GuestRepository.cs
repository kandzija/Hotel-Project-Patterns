using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WindowsFormsHotel.Code
{
    public class GuestRepository : IGuestRepository
    {
        public Guest Get(Guid id)
        {
            Guest entity = null;
            using (HotelContext context = new HotelContext())
            {
                entity = context.Guest.FirstOrDefault(p => p.Id == id);
            }

            return entity;
        }

        public List<Guest> Get(GuestFilter filter = null)
        {
            List<Guest> toReturn = null;

            using (HotelContext context = new HotelContext())
            {
                var query = context.Guest.AsQueryable();

                if (filter != null)
                {
                    if (filter.Id.HasValue)
                    {
                        query = query.Where(p => p.Id == filter.Id.Value);
                    }

                    if (!String.IsNullOrEmpty(filter.Text))
                    {
                        query = query.Where(p => p.FirstName.ToString().Contains(filter.Text) ||
                            p.LastName.ToString().Contains(filter.Text) ||
                            p.PIN.ToString().Contains(filter.Text) ||
                            p.Address.ToString().Contains(filter.Text) ||
                            p.Phone.ToString().Contains(filter.Text));
                    }

                }

                toReturn = query.ToList();
            }

            return toReturn;
        }

        public Guest Create(Guest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            bool result = false;
            Guest toReturn = null;

            model.Id = Guid.NewGuid();
            model.DateCreated = 
            model.DateUpdated = DateTime.Now;

            using (HotelContext context = new HotelContext())
            {
                context.Guest.Add(model);
                result = context.SaveChanges() > 0;

                if (result)
                {
                    toReturn = Get(model.Id);
                }
            }

            return toReturn;
        }

        public bool Update(Guest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            Guest entity = null;
            bool result = false;

            using (HotelContext context = new HotelContext())
            {
                entity = context.Guest.Find(model.Id);

                if (entity != null)
                {
                    entity.HotelId = model.HotelId;
                    entity.FirstName = model.FirstName;
                    entity.LastName = model.LastName;
                    entity.PIN = model.PIN;
                    entity.Address = model.Address;
                    entity.Phone = model.Phone;
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
                Guest toDelete = context.Guest.Find(id);

                if (toDelete != null)
                {
                    context.Guest.Remove(toDelete);
                }

                result = context.SaveChanges() > 0;
            }

            return result;
        }
    }
}