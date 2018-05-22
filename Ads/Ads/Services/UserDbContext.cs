using Ads.Database;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Ads.Services
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        public List<User> FindByEmail(string email)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Users.Where(s => s.Email == email).ToList();
                if (item == null)
                {
                    throw new NullReferenceException($"User {email} not found");
                }
                return item;
            }
        }

        public User GetById(int userId)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Users.Where(s => s.Id == userId).FirstOrDefault();
                if (item == null)
                {
                    throw new NullReferenceException($"User {userId} not found");
                }
                return item;
            }
        }

        public User Add(User user)
        {
            using (var context = new AdvertsEntities())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return GetById(user.Id);
        }

        public User Update(User user)
        {
            var item = GetById(user.Id);
            if (item == null)
            {
                throw new NullReferenceException($"User {user.Id} not found");
            }

            using (var context = new AdvertsEntities())
            {
                context.Entry(item).CurrentValues.SetValues(user);
            }

            return user;
        }

        public void Delete(int advertId)
        {
            var item = GetById(advertId);
            if (item == null)
            {
                throw new NullReferenceException($"Advert {advertId} not found");
            }

            using (var context = new AdvertsEntities())
            {
                context.Users.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
