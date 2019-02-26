using OdeToFood.Core;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        //IEnumerable<Restaurant> GetAll();

        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int Id);

        Restaurant Update(Restaurant updatedRestaurant);

        Restaurant Add(Restaurant newRestaurant);
        int Commit();

        Restaurant Delete(int id);

        
        int GetCountOfRestaurants();
    }
}
