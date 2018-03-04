using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Restaurant
{
    public string name;
    public int rating;
    public int totalMeals;
    public Dictionary<string, int> specialMeals = new Dictionary<string, int>();

    public Restaurant() { }

    public Restaurant(string n, int r, int t, Dictionary<string, int> s)
    {
        name = n;
        rating = r;
        totalMeals = t;
        specialMeals = s;
    }
}

