using System;
using System.Linq;

namespace WindowsFormsHotel.Code
{
    public class HotelRepository : IHotelRepository
    {
        public Hotel Get()
        {
            Hotel entity = null;
            using (HotelContext context = new HotelContext())
            {
                //TODO primjetite da ovdje ne proslijeđujemo Id i ne radimo filter po Id-u. Razlog je što ćemo uvijek u bazi podataka imati samo jedan hotel koji koristimo za aplikaciju.
                entity = context.Hotel.FirstOrDefault();
            }

            return entity;
        }

        public bool Update(Hotel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            Hotel entity = null;
            bool result = false;

            using (HotelContext context = new HotelContext())
            {

                entity = context.Hotel.Find(model.Id);

                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.Phone = model.Phone;
                    entity.DateUpdated = DateTime.Now;

                    result = context.SaveChanges() > 0;
                }
            }

            return result;
        }

        public Hotel Create(Hotel model)
        {
            return model;
        }
    }
}