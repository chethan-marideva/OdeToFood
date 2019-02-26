using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        public IHtmlHelper htmlhelper;

        private readonly IRestaurantData restaurantData;

      

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cusines { get; set; }
        public EditModel(IRestaurantData restaurantData,IHtmlHelper htmlHelper)
        {
            this.htmlhelper = htmlHelper;
            this.restaurantData = restaurantData;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cusines = htmlhelper.GetEnumSelectList<CusineType>();

            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else { Restaurant = new Restaurant(); }

            if (Restaurant == null) { return RedirectToPage("./NotFound"); }

            return Page();

        }

      public IActionResult OnPost() {


            if (!ModelState.IsValid) {

                Cusines = htmlhelper.GetEnumSelectList<CusineType>();
                return Page();
                
            }

            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
                TempData["Message"] = "Updated Sucessfully !";
            }
            else
            {
                restaurantData.Add(Restaurant);
                TempData["Message"] = "Added Sucessfully !";
            }
            restaurantData.Commit();

            

            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });


        }
    }
}