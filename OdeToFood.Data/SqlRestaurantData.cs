using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        public OdeToFoodDbContext db { get; }
        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

       

         Restaurant IRestaurantData.Add(Restaurant newRestaurant)
        {
            db.Restaurants.Add(newRestaurant);
            return newRestaurant;
        }

         int IRestaurantData.Commit()
        {
            return db.SaveChanges();
        }

         Restaurant IRestaurantData.Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null) { db.Restaurants.Remove(restaurant); }

            return restaurant;
        }

       

         public Restaurant GetById(int Id)
        {
            return db.Restaurants.Find(Id);
        }

         IEnumerable<Restaurant> IRestaurantData.GetRestaurantsByName(string name)
        {
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;

            return query;
        }

         Restaurant IRestaurantData.Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return updatedRestaurant;
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }
    }
}
