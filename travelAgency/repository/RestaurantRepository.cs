using System.Collections.Generic;
using System.Linq;
using travelAgency.model;

namespace travelAgency.repository;

public class RestaurantRepository
{
    private readonly TravelAgencyContext dbContext;

    public RestaurantRepository(TravelAgencyContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Restaurant GetById(int id)
    {
        return dbContext.Restaurants.FirstOrDefault(u => u.Id == id && !u.IsDeleted);
    }

    public List<Restaurant> GetAll()
    {
        return dbContext.Restaurants
             .Where(t => !t.IsDeleted)
             .ToList();
    }

    public void Add(Restaurant restaurant)
    {
        restaurant.IsDeleted = false;
        dbContext.Restaurants.Add(restaurant);
        dbContext.SaveChanges();
    }

    public void Update(Restaurant restaurant)
    {
        dbContext.Restaurants.Update(restaurant);
        dbContext.SaveChanges();
    }

    public void Delete(Restaurant restaurant)
    {
        restaurant.IsDeleted = true;
        dbContext.Restaurants.Update(restaurant);
        dbContext.SaveChanges();
    }
}