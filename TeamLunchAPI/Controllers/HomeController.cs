using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeamLunchAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        // GET request which returns the optimal meal list.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Meals")]
        public Dictionary<KeyValuePair<string, string>, string> GetMeals()
        {
            List<Restaurant> sorted = Data.Instance.Restaurants.OrderByDescending(restaurant => restaurant.rating).ToList(); // sort for highest rated stuff first
            // unit test for meal subtraction (is it working on sorted or original list? should be on copy)

            Dictionary<string, string> people = Data.Instance.TeamMembers;      // remove people from this dictionary as their meal is found
            List<string> peopleToRemove = new List<string>();                   // list of ids to remove from people dictionary

            // Dictionary to illustrate which people get what meals from where.
            // Contains people keys (keyvaluepairs- id and diet restr.) and restaurant name values.
            Dictionary<KeyValuePair<string, string>, string> mealOrder = new Dictionary<KeyValuePair<string, string>, string>();

            foreach (Restaurant restaurant in sorted)
            {
                foreach (KeyValuePair<string, string> teamMember in people)
                {
                    if (restaurant.totalMeals > 0)
                    {
                        if (teamMember.Value == string.Empty)
                        {
                            // assign this person a meal, no dietary restrictions
                            mealOrder.Add(teamMember, restaurant.name);
                            restaurant.totalMeals--;
                            System.Diagnostics.Debug.Write("+++ TeamMember " + teamMember.Key + " ASSIGNED REGULAR MEAL");
                            peopleToRemove.Add(teamMember.Key);
                        }
                        else // the person has dietary restrictions, see if the restaurant can accommodate them
                        {
                            string dietRestr = teamMember.Value;
                            if (restaurant.specialMeals[dietRestr] > 0)
                            {
                                mealOrder.Add(teamMember, restaurant.name);
                                restaurant.totalMeals--;
                                restaurant.specialMeals[dietRestr]--;
                                System.Diagnostics.Debug.Write(">>> TeamMember " + teamMember.Key + " ASSIGNED " + teamMember.Value + " MEAL");
                                peopleToRemove.Add(teamMember.Key);
                            }
                            else
                            {
                                // handle the possibility for diet restricted people not getting a meal?
                            }
                        }
                    }
                    else
                        break;
                }

                // Update who we have accounted for
                foreach (string id in peopleToRemove)
                {
                    people.Remove(id);
                }
                peopleToRemove.Clear();

                // Break and return if we've given everyone a meal
                if (people.Count <= 0)
                    break;
            }
            return mealOrder;
        }
    }
}
