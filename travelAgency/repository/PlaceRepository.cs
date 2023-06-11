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
        return dbContext.Places.FirstOrDefault(u => u.Id == id);
    }

    public List<Place> GetAll()
    {
        return dbContext.Places.ToList();
    }

    public void Add(Place place)
    {
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
        dbContext.Places.Remove(place);
        dbContext.SaveChanges();
        }
        public void Save()
        {
            dbContext.SaveChanges();
    }


    }
}