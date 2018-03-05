using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeamLunchAPI.Controllers
{
    [Produces("application/json")]
    [Route("/Restaurant")]
    public class RestaurantController : Controller
    {
        // GET: /Restaurant
        [HttpGet]
        public List<Restaurant> GetAll()
        {
            return Data.Instance.Restaurants;
        }

        // GET request to return a restaurant with specific name.
        [HttpGet]
        [Route("/Restaurant/Get")]
        public Restaurant Get([FromHeader] string name)
        {
            //return Data.Instance.Restaurants.FirstOrDefault(restaurant => name == restaurant.name);
            return Data.Instance.GetRestaurant(name);
        }

        // POST request to add a new restaurant.
        [HttpPost]
        [Route("/Restaurant/Add")]
        public IActionResult Add([FromHeader] string name,
                                 [FromHeader] string rating,
                                 [FromHeader] string totalMeals,
                                 [FromHeader] string numV,
                                 [FromHeader] string numG,
                                 [FromHeader] string numN,
                                 [FromHeader] string numF)
        {
            if (Data.Instance.GetRestaurant(name) == null)
            {
                Restaurant newR = new Restaurant();
                // If each numerical property can be parsed correctly, set it.
                // Else, set it to an arbitrary error marker (i.e. -1).
                newR.name = name;
                int.TryParse(rating, out int ratingInt);
                newR.rating = (ratingInt <= 5 && ratingInt >= 0) ? ratingInt : -1;  // Ratings must be inclusively between 0 and 5
                newR.totalMeals = (int.TryParse(totalMeals, out int totalMealsInt)) ? totalMealsInt : -1;
                newR.specialMeals["v"] = (int.TryParse(numV, out int numVInt)) ? numVInt : -1;
                newR.specialMeals["g"] = (int.TryParse(numG, out int numGInt)) ? numGInt : -1;
                newR.specialMeals["n"] = (int.TryParse(numN, out int numNInt)) ? numNInt : -1;
                newR.specialMeals["f"] = (int.TryParse(numF, out int numFInt)) ? numFInt : -1;

                Data.Instance.Restaurants.Add(newR);
                return Ok();
            }
            else
                return BadRequest();
        }

        // PUT request to edit an existing restaurant.
        // Restaurant names cannot be edited (create a new restaurant with different name).
        [HttpPut]
        [Route("/Restaurant/Edit")]
        public IActionResult Edit([FromHeader] string name,
                                  [FromHeader] string rating,
                                  [FromHeader] string totalMeals,
                                  [FromHeader] string numV,
                                  [FromHeader] string numG,
                                  [FromHeader] string numN,
                                  [FromHeader] string numF)
        {
            Restaurant r = Data.Instance.GetRestaurant(name);
            if (r != null)
            {
                // If each numerical property can be parsed correctly, set it.
                // Else, set it to an arbitrary error marker (i.e. -1).
                int.TryParse(rating, out int ratingInt);
                r.rating = (ratingInt <= 5 && ratingInt >= 0) ? ratingInt : -1;  // Ratings must be inclusively between 0 and 5
                r.totalMeals = (int.TryParse(totalMeals, out int totalMealsInt)) ? totalMealsInt : -1;
                r.specialMeals["v"] = (int.TryParse(numV, out int numVInt)) ? numVInt : -1;
                r.specialMeals["g"] = (int.TryParse(numG, out int numGInt)) ? numGInt : -1;
                r.specialMeals["n"] = (int.TryParse(numN, out int numNInt)) ? numNInt : -1;
                r.specialMeals["f"] = (int.TryParse(numF, out int numFInt)) ? numFInt : -1;

                return Ok();
            }
            else
                return BadRequest();
        }

        // DELETE request to delete an existing restaurant.
        [HttpDelete]
        [Route("/Restaurant/Delete")]
        public IActionResult Delete([FromHeader] string name)
        {
            Restaurant r = Data.Instance.GetRestaurant(name);
            if (r != null)
            {
                Data.Instance.Restaurants.Remove(r);
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}
