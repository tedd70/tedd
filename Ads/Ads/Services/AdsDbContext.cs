using Ads.Database;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Ads.Services
{
    public class AdsDbContext : DbContext, IAdsDbContext
    {
        public Advert GetById(int advertId)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Adverts.Where(s => s.Id == advertId).FirstOrDefault();
                if (item == null)
                {
                    throw new NullReferenceException($"Advert {advertId} not found");
                }
                return item;
            }
        }

        public List<Advert> GetAll()
        {
            using (var context = new AdvertsEntities())
            {
                return context.Adverts.ToList();
            }
        }

        public Advert Add(Advert advert)
        {
            using (var context = new AdvertsEntities())
            {
                context.Adverts.Add(advert);
                context.SaveChanges();
            }

            return GetById(advert.Id);
        }

        public Advert Update(Advert advert)
        {
            using (var context = new AdvertsEntities())
            {
                var item = context.Adverts.Where(s => s.Id == advert.Id).FirstOrDefault();
                if (item == null)
                {
                    throw new NullReferenceException($"Advert {advert.Id} not found");
                }
                context.Entry(item).CurrentValues.SetValues(advert);
                context.SaveChanges();
            }

            return advert;
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
                context.Adverts.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
