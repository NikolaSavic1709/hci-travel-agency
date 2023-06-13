using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.repository
{
    public class ArrangementRepository
    {
        private readonly TravelAgencyContext dbContext;

        public ArrangementRepository(TravelAgencyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Arrangement GetById(int id)
        {
            return dbContext.Arrangements
                .AsNoTracking()
                .Include(a => a.Trip)
                .FirstOrDefault(u => u.Id == id);
        }

        public List<Arrangement> GetAll()
        {
            //return dbContext.Arrangements.ToList();
            return dbContext.Arrangements.Include(a => a.Trip).Include(a => a.User).ToList();
        }

        public List<Arrangement> GetArrangementsForUser(int userId)
        {
            return dbContext.Arrangements
                .Include(a => a.Trip)
                .Where(a => a.UserId == userId)
                .ToList();
        }

        public void Add(Arrangement arrangement)
        {
            dbContext.Arrangements.Add(arrangement);
            dbContext.SaveChanges();
        }

        public void Update(Arrangement arrangement)
        {          
            dbContext.Arrangements.Update(arrangement);
            dbContext.SaveChanges();
        }

        public void Delete(Arrangement arrangement)
        {
            dbContext.Arrangements.Remove(arrangement);
            dbContext.SaveChanges();
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
