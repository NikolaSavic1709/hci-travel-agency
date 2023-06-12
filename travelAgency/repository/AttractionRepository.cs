using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.repository
{
    public class AttractionRepository
    {
        private readonly TravelAgencyContext dbContext;

        public AttractionRepository(TravelAgencyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Attraction GetById(int id)
        {
            return dbContext.Attractions.FirstOrDefault(u => u.Id == id && !u.IsDeleted);
        }

        public List<Attraction> GetAll()
        {
            return dbContext.Attractions
                .Where(t => !t.IsDeleted)
                .ToList();
        }

        public void Add(Attraction attraction)
        {
            attraction.IsDeleted = false;
            dbContext.Attractions.Add(attraction);
            dbContext.SaveChanges();
        }

        public void Update(Attraction attraction)
        {
            dbContext.Attractions.Update(attraction);
            dbContext.SaveChanges();
        }

        public void Delete(Attraction attraction)
        {
            attraction.IsDeleted = true;
            dbContext.Attractions.Update(attraction);
            dbContext.SaveChanges();
        }
    }
}
