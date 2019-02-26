using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>() {

                new Restaurant {Id=1,Name="Dominoz",Location="JP Nagar",Cusine=CusineType.Italian},
                new Restaurant{Id=2,Name="Taco Bell",Location="BG Road",Cusine=CusineType.Mexican},
                new Restaurant {Id=3,Name="BBQ Nation",Location="ECity",Cusine=CusineType.Indian},
                new Restaurant {Id=4,Name="Empire",Location="JP Nagar",Cusine=CusineType.Indian},
                new Restaurant {Id=5,Name="Hakuna Matata",Location="JP Nagar",Cusine=CusineType.Continental}
            };
        }

        public Restaurant GetById(int Id)
        {
            return restaurants.SingleOrDefault(r => r.Id == Id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name=null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        
        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);

            if (restaurant != null) {

                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cusine = updatedRestaurant.Cusine;
            }

            return restaurant;
        }


        public int Commit() { return 0; }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id ==id);

            if (restaurant != null) { restaurants.Remove(restaurant); }

            return restaurant;

        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}
