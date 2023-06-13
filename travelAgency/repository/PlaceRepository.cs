using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.repository
{
    public class PlaceRepository
    {
        private readonly TravelAgencyContext dbContext;

        public PlaceRepository(TravelAgencyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Place GetById(int id)
        {
            return dbContext.Places.FirstOrDefault(u => u.Id == id && !u.IsDeleted);
        }

        public List<Place> GetAll()
        {
            return dbContext.Places
                 .Where(t => !t.IsDeleted)
                 .ToList();
        }

        public void Add(Place place)
        {
            place.IsDeleted = false;
            dbContext.Places.Add(place);
            dbContext.SaveChanges();
        }

        public void Update(Place place)
        {
            dbContext.Places.Update(place);
            dbContext.SaveChanges();
        }

        public void Delete(Place place)
        {
            place.IsDeleted = true;
            dbContext.Places.Update(place);
            dbContext.SaveChanges();
        }
        public void Save()
        {
            dbContext.SaveChanges();
    }


    }
}
