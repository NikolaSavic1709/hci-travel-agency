﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.repository
{
    public class UserRepository
    {
        private readonly TravelAgencyContext dbContext;

        public UserRepository(TravelAgencyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetById(int id)
        {
            return dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAll()
        {
            return dbContext.Users.ToList();
        }

        public void Add(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public void Update(User user)
        {
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
        }

        public void Delete(User user)
        {
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
