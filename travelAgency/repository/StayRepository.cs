using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.repository
{
    public class StayRepository
    {
        private readonly TravelAgencyContext dbContext;

        public StayRepository(TravelAgencyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Stay GetById(int id)
        {
            return dbContext.Stays
                .AsNoTracking()
                .Include(s => s.StayAmenities)
                .FirstOrDefault(u => u.Id == id && !u.IsDeleted);
        }

        public List<Stay> GetAll()
        {
            return dbContext.Stays
                .Where(t => !t.IsDeleted)
                .ToList();
        }

        public void Add(Stay stay)
        {
            stay.IsDeleted = false;
            dbContext.Stays.Add(stay);
            dbContext.SaveChanges();
        }

        public void Update(Stay stay)
        {
            var stayAmenitiesToRemove = dbContext.Amenities.Where(a => a.StayId == stay.Id);
            dbContext.Amenities.RemoveRange(stayAmenitiesToRemove);

            // Update the Stay entity
            dbContext.Stays.Update(stay);
            dbContext.SaveChanges();
        }

        public void Delete(Stay stay)
        {
            stay.IsDeleted = true;
            dbContext.Stays.Update(stay);
            dbContext.SaveChanges();
        }
    }
}
