using System.Collections.Generic;
using System.Linq;
using travelAgency.model;

namespace travelAgency.repository;

public class StayRepository
{
    private readonly TravelAgencyContext dbContext;

    public StayRepository(TravelAgencyContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Stay GetById(int id)
    {
        return dbContext.Stays.FirstOrDefault(u => u.Id == id);
    }

    public List<Stay> GetAll()
    {
        return dbContext.Stays.ToList();
    }

    public void Add(Stay stay)
    {
        dbContext.Stays.Add(stay);
        dbContext.SaveChanges();
    }

    public void Update(Stay stay)
    {
        dbContext.Stays.Update(stay);
        dbContext.SaveChanges();
    }

    public void Delete(Stay stay)
    {
        dbContext.Stays.Remove(stay);
        dbContext.SaveChanges();
    }
}