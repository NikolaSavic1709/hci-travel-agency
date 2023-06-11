﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using travelAgency.model;

namespace travelAgency.repository
{
    public class TripRepository
    {
        private readonly TravelAgencyContext dbContext;

        public TripRepository(TravelAgencyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Trip GetById(int id)
        {
            return dbContext.Trips.FirstOrDefault(u => u.Id == id);
        }

        public List<Trip> GetAll()
        {
            return dbContext.Trips.Include(t => t.Schedules).ThenInclude(s=>s.Place).ToList();
        }

        public void Add(Trip trip)
        {
            dbContext.Trips.Add(trip);
            dbContext.SaveChanges();
        }

        public void Update(Trip trip)
        {
            dbContext.Trips.Update(trip);
            dbContext.SaveChanges();
        }

        public void Delete(Trip trip)
        {
            dbContext.Trips.Remove(trip);
            dbContext.SaveChanges();
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }
        
    }
}