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
            return dbContext.Attractions.FirstOrDefault(u => u.Id == id);
        }

        public List<Attraction> GetAll()
        {
            return dbContext.Attractions.ToList();
        }

        public void Add(Attraction attraction)
        {
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
            dbContext.Attractions.Remove(attraction);
            dbContext.SaveChanges();
        }
    }
}
