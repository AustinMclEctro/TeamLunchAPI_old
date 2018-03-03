using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Restaurant
{
    public string name;
    public int rating;
    public int totalMeals;
    public Dictionary<char, int> specialMeals = new Dictionary<char, int>();

    public Restaurant(string n, int r, int t, Dictionary<char, int> s)
    {
        name = n;
        rating = r;
        totalMeals = t;
        specialMeals = s;
    }
}

